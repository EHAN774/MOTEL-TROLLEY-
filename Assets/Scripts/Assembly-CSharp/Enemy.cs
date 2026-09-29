using System;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	public enum EnemyState
	{
		Patrol = 0,
		Chase = 1,
		Attack = 2,
		FinalPhase = 3,
		Die = 4
	}

	private GameObject playerObj;

	private NavMeshAgent agent;

	private EnemySounds sounds;

	[Header("EnemyStatus")]
	[SerializeField]
	private float defaultSpeed = 1.5f;

	[SerializeField]
	private float runSpeed = 2f;

	[SerializeField]
	private float enemyMaxSpeed = 2.5f;

	[Space(5f)]
	[Header("Patrol")]
	[Tooltip("Enemy Patrol Distance")]
	public float ObservePatDistance = 1f;

	[SerializeField]
	private float patrolStopTime = 3f;

	[SerializeField]
	private float patrolTimer;

	public List<GameObject> PatrolPoint;

	private int patPoint;

	private float patDistance;

	[Space(5f)]
	[Header("Chase")]
	[SerializeField]
	public bool isChasing;

	[Space(5f)]
	[Header("Animation")]
	public GameObject _model;

	private Animator animator;

	[Space(5f)]
	[Header("Attack")]
	public float attackRange = 1f;

	public float attackTimer = 2f;

	public float curAttackTime = 2f;

	public bool isAttack;

	private float distance;

	[Space(5f)]
	[Header("FinalPhase")]
	[SerializeField]
	public bool isFinalPhase;

	[Space(5f)]
	[Header("EnemyView")]
	[SerializeField]
	private bool DebugMode;

	[Range(0f, 360f)]
	[SerializeField]
	public float ViewAngle;

	[SerializeField]
	public float ViewRadius = 1f;

	[SerializeField]
	private LayerMask TargetMask;

	[SerializeField]
	private LayerMask ObstacleMask;

	public float defaultAngle = 60f;

	public float defaultRadius = 3f;

	private List<Collider> hitTargetList;

	private bool isLockOn;

	private float ObserveDistance = 5f;

	public EnemyState state;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		playerObj = GameObject.FindWithTag("Player");
		hitTargetList = new List<Collider>();
		animator = _model.GetComponent<Animator>();
		sounds = GetComponent<EnemySounds>();
		state = EnemyState.Patrol;
		agent.speed = defaultSpeed;
		patPoint = 0;
		ViewAngle = defaultAngle;
		ViewRadius = defaultRadius;
		isChasing = false;
		isLockOn = false;
		defaultAngle = 60f;
		defaultRadius = 3f;
		isFinalPhase = false;
	}

	private void Update()
	{
		patDistance = Vector3.Distance(base.transform.position, PatrolPoint[patPoint].transform.position);
		distance = Vector3.Distance(playerObj.transform.position, base.transform.position);
		bool flag = Looking();
		if (!isFinalPhase && !isAttack && playerObj.transform.position.y - 0.3f <= base.transform.position.y && base.transform.position.y <= playerObj.transform.position.y + 0.3f)
		{
			if (flag)
			{
				isChasing = true;
				isLockOn = true;
				state = EnemyState.Chase;
			}
			else if (isLockOn && Vector3.Distance(base.transform.position, playerObj.transform.position) <= ObserveDistance)
			{
				isChasing = true;
				state = EnemyState.Chase;
			}
			else
			{
				isChasing = false;
				isLockOn = false;
				state = EnemyState.Patrol;
			}
		}
		switch (state)
		{
		case EnemyState.Chase:
			EnemyChase();
			break;
		case EnemyState.Patrol:
			EnemyPatrol(patPoint);
			break;
		case EnemyState.Attack:
			EnemyAttack();
			break;
		case EnemyState.FinalPhase:
			EnemyFinalPhase();
			break;
		case EnemyState.Die:
			EnemyDie();
			break;
		}
	}

	private void EnemyChase()
	{
		animator.SetBool("Idle", value: false);
		animator.SetBool("Walking", value: false);
		animator.SetBool("Running", value: true);
		agent.speed = runSpeed;
		playerObj.GetComponent<FirstPersonController>().StartBreath();
		if (playerObj.transform.position.y - 0.3f <= base.transform.position.y && base.transform.position.y <= playerObj.transform.position.y + 0.3f)
		{
			agent.transform.LookAt(new Vector3(playerObj.transform.position.x, base.transform.position.y, playerObj.transform.position.z));
		}
		agent.destination = playerObj.transform.position;
		if (distance < attackRange)
		{
			isAttack = true;
			state = EnemyState.Attack;
		}
		if (isFinalPhase)
		{
			state = EnemyState.FinalPhase;
		}
	}

	private void EnemyAttack()
	{
		agent.destination = agent.transform.position;
		curAttackTime += Time.deltaTime;
		if (curAttackTime > attackTimer)
		{
			animator.SetTrigger("Attack");
			curAttackTime = 0f;
			sounds.PlayAttackSound();
			playerObj.GetComponent<FirstPersonController>().PlayerHit();
		}
		isAttack = false;
		state = EnemyState.Chase;
	}

	private void EnemyPatrol(int _patPoint)
	{
		agent.destination = PatrolPoint[_patPoint].transform.position;
		agent.speed = defaultSpeed;
		if (patDistance >= ObservePatDistance)
		{
			animator.SetBool("Idle", value: false);
			animator.SetBool("Walking", value: true);
			animator.SetBool("Running", value: false);
		}
		else
		{
			if (!(patDistance < ObservePatDistance))
			{
				return;
			}
			animator.SetBool("Idle", value: true);
			animator.SetBool("Walking", value: false);
			animator.SetBool("Running", value: false);
			patrolTimer += Time.deltaTime;
			if (patrolTimer >= patrolStopTime)
			{
				animator.SetBool("Idle", value: false);
				animator.SetBool("Walking", value: true);
				animator.SetBool("Running", value: false);
				patPoint++;
				patrolTimer = 0f;
				if (patPoint == PatrolPoint.Count)
				{
					patPoint = 0;
				}
			}
		}
	}

	private void EnemyFinalPhase()
	{
		animator.SetBool("Idle", value: false);
		animator.SetBool("Walking", value: false);
		animator.SetBool("Running", value: true);
		agent.speed = enemyMaxSpeed;
		if (Vector3.Distance(base.transform.position, playerObj.transform.position) <= 8f && Mathf.Abs(base.transform.position.y - playerObj.transform.position.y) <= 0.3f)
		{
			agent.transform.LookAt(new Vector3(playerObj.transform.position.x, base.transform.position.y, playerObj.transform.position.z));
		}
		if (distance <= attackRange)
		{
			isAttack = true;
			state = EnemyState.Attack;
		}
		agent.destination = playerObj.transform.position;
	}

	private Vector3 AngleToDir(float angle)
	{
		float f = angle * (MathF.PI / 180f);
		return new Vector3(Mathf.Sin(f), 0f, Mathf.Cos(f));
	}

	private bool Looking()
	{
		Vector3 vector = base.transform.position + Vector3.up * 0.5f;
		float y = base.transform.eulerAngles.y;
		Vector3 vector2 = AngleToDir(base.transform.eulerAngles.y + ViewAngle * 0.5f);
		Vector3 vector3 = AngleToDir(base.transform.eulerAngles.y - ViewAngle * 0.5f);
		Vector3 vector4 = AngleToDir(y);
		if (DebugMode)
		{
			Debug.DrawRay(vector, vector2 * ViewRadius, Color.blue);
			Debug.DrawRay(vector, vector3 * ViewRadius, Color.blue);
			Debug.DrawRay(vector, vector4 * ViewRadius, Color.cyan);
		}
		hitTargetList.Clear();
		Collider[] array = Physics.OverlapSphere(vector, ViewRadius, TargetMask);
		if (array.Length == 0)
		{
			return false;
		}
		Collider[] array2 = array;
		foreach (Collider collider in array2)
		{
			Vector3 position = collider.transform.position;
			Vector3 normalized = (position - vector).normalized;
			if (Mathf.Acos(Vector3.Dot(vector4, normalized)) * 57.29578f <= ViewAngle * 0.5f && !Physics.Raycast(vector, position, ViewRadius, ObstacleMask))
			{
				hitTargetList.Add(collider);
				if (DebugMode)
				{
					Debug.DrawLine(vector, position, Color.red);
				}
			}
		}
		return true;
	}

	private void EnemyDie()
	{
		Debug.Log("Enemy Dead");
		base.gameObject.SetActive(value: false);
	}
}
