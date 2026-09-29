using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StarterAssets
{
	[RequireComponent(typeof(CharacterController))]
	[RequireComponent(typeof(PlayerInput))]
	public class FirstPersonController : MonoBehaviour
	{
		[Header("Player")]
		[Tooltip("Move speed of the character in m/s")]
		public float MoveSpeed = 2f;

		[Tooltip("Sprint speed of the character in m/s")]
		public float SprintSpeed = 3f;

		[Tooltip("Rotation speed of the character")]
		public float RotationSpeed = 1.5f;

		[Tooltip("Acceleration and deceleration")]
		public float SpeedChangeRate = 10f;

		[Tooltip("is Player Sitting")]
		public bool Sitting;

		[Tooltip("Player Sit Height")]
		public float SitHeight = 0.75f;

		[Tooltip("Player Stand Height")]
		public float StandHeight = 2f;

		private float curMoveSpeed;

		private float curSprintSpeed;

		[Space(10f)]
		[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
		public float Gravity = -15f;

		[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
		public float FallTimeout = 0.15f;

		[Header("Player Grounded")]
		[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
		public bool Grounded = true;

		[Tooltip("Useful for rough ground")]
		public float GroundedOffset = -0.14f;

		[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
		public float GroundedRadius = 0.5f;

		[Tooltip("What layers the character uses as ground")]
		public LayerMask GroundLayers;

		[Header("Cinemachine")]
		[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
		public GameObject CinemachineCameraTarget;

		[Tooltip("How far in degrees can you move the camera up")]
		public float TopClamp = 90f;

		[Tooltip("How far in degrees can you move the camera down")]
		public float BottomClamp = -90f;

		public float _cinemachineTargetPitch;

		private float _speed;

		private float _rotationVelocity;

		private float _verticalVelocity;

		private float _terminalVelocity = 53f;

		private float _fallTimeoutDelta;

		[Space(10f)]
		[Header("FallingAnim")]
		[SerializeField]
		private bool isFalling;

		private Animator _cameraAnim;

		public GameObject _playerFollowCam;

		public AudioClip[] _fallBreathClip;

		private PlayerInput _playerInput;

		private CharacterController _controller;

		private StarterAssetsInputs _input;

		private GameObject _mainCamera;

		private const float _threshold = 0.01f;

		[Space(5f)]
		[Header("RayCast")]
		private RaycastHit hit;

		[SerializeField]
		private float RayDist = 2f;

		[Space(5f)]
		[Header("UIs")]
		public GameObject PressFImg;

		public GameObject MoveInfoImg;

		public GameObject FlashInfoImg;

		public GameObject CandleImg;

		public GameObject RunInfoImg;

		public TextMeshProUGUI LastCandleTxt;

		public TextMeshProUGUI HaveCandleTxt;

		public TextMeshProUGUI NeedKeyTxt;

		[Space(5f)]
		[Header("CameraShake")]
		private Animator _playerAnim;

		private bool isWalking;

		[Space(5f)]
		[Header("Run and Walk")]
		private bool canMove = true;

		public AudioClip[] _footStepClips;

		private float footStepTimer = 1f;

		private float curFootStepTime;

		[Space(5f)]
		[Header("Candle")]
		[SerializeField]
		private bool isHavingCandle;

		[SerializeField]
		private AudioClip candleClip;

		[Space(5f)]
		[Header("FlashLight")]
		private bool isHavingFlashLight;

		public GameObject FlashLight;

		[SerializeField]
		private AudioClip flashClip;

		[Space(5f)]
		[Header("Enemy")]
		private Enemy _enemy;

		public AudioClip[] _Heartclips;

		public AudioClip[] _BreatheClips;

		private AudioSource _audio;

		private float bounceTimer = 4f;

		private float curTime;

		private float breathTimer = 3.5f;

		private float breathTime;

		public bool isPause;

		[Space(5f)]
		[Header("FenceKey")]
		public bool isHavingKey;

		[SerializeField]
		private AudioClip FenceLocked;

		[SerializeField]
		private AudioClip FenceKeyGet;

		[Space(5f)]
		[Header("HP")]
		private int MaxHP;

		[SerializeField]
		public int curHP;

		public GameObject[] Hearts;

		private int heartIdx;

		[SerializeField]
		private AudioClip DieScream;

		[Space(5f)]
		[SerializeField]
		private LoadingPanel _loadingPanel;

		private bool IsCurrentDeviceMouse => _playerInput.currentControlScheme == "KeyboardMouse";

		private void Awake()
		{
			if (_mainCamera == null)
			{
				_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
			}
		}

		private void Start()
		{
			_controller = GetComponent<CharacterController>();
			_input = GetComponent<StarterAssetsInputs>();
			_playerInput = GetComponent<PlayerInput>();
			_playerAnim = GetComponent<Animator>();
			_cameraAnim = _playerFollowCam.GetComponent<Animator>();
			_enemy = Object.FindObjectOfType<Enemy>();
			_audio = GetComponent<AudioSource>();
			_loadingPanel = _loadingPanel.GetComponent<LoadingPanel>();
			_fallTimeoutDelta = FallTimeout;
			PressFImg.SetActive(value: false);
			MoveInfoImg.SetActive(value: false);
			FlashInfoImg.SetActive(value: false);
			CandleImg.SetActive(value: false);
			RunInfoImg.SetActive(value: false);
			LastCandleTxt.gameObject.SetActive(value: false);
			HaveCandleTxt.gameObject.SetActive(value: false);
			MaxHP = GameManager.Instance.MaxHP;
			curHP = MaxHP;
			GameObject[] hearts = Hearts;
			for (int i = 0; i < hearts.Length; i++)
			{
				hearts[i].gameObject.SetActive(value: false);
			}
			isHavingCandle = false;
			isHavingFlashLight = false;
			isHavingKey = false;
			heartIdx = 0;
		}

		private void Update()
		{
			if (!GameManager.Instance._isGameStart)
			{
				return;
			}
			if (MenuManager.Instance != null)
			{
				RotationSpeed = MenuManager.Instance._mouseSensibility * 3f;
				AudioListener.volume = MenuManager.Instance._volumeValue;
			}
			else
			{
				RotationSpeed = 1f;
				AudioListener.volume = 1f;
			}
			if (!isPause)
			{
				JumpAndGravity();
				GroundedCheck();
				if (canMove)
				{
					Move();
				}
				SitDown();
				FindObj();
				HeartBounce();
				if (isHavingCandle)
				{
					CandleImg.SetActive(value: true);
				}
				else
				{
					CandleImg.SetActive(value: false);
				}
			}
		}

		private void LateUpdate()
		{
			if (GameManager.Instance._isGameStart && !isPause)
			{
				CameraRotation();
			}
		}

		private void GroundedCheck()
		{
			Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y - GroundedOffset, base.transform.position.z);
			Grounded = Physics.CheckSphere(position, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
		}

		private void CameraRotation()
		{
			if (_input.look.sqrMagnitude >= 0.01f)
			{
				float num = (IsCurrentDeviceMouse ? 1f : Time.deltaTime);
				_cinemachineTargetPitch += _input.look.y * RotationSpeed * num;
				_rotationVelocity = _input.look.x * RotationSpeed * num;
				_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);
				CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0f, 0f);
				base.transform.Rotate(Vector3.up * _rotationVelocity);
			}
		}

		private void Move()
		{
			if (isWalking)
			{
				curFootStepTime += Time.deltaTime;
				if (curFootStepTime >= footStepTimer)
				{
					int num = Random.Range(0, _footStepClips.Length);
					_audio.clip = _footStepClips[num];
					_audio.PlayOneShot(_audio.clip);
					curFootStepTime = 0f;
				}
				if (_input.sprint)
				{
					_playerAnim.SetBool("IsIdle", value: false);
					_playerAnim.SetBool("IsWalking", value: false);
					_playerAnim.SetBool("IsRunning", value: true);
				}
				else
				{
					_playerAnim.SetBool("IsIdle", value: false);
					_playerAnim.SetBool("IsWalking", value: true);
					_playerAnim.SetBool("IsRunning", value: false);
				}
			}
			else
			{
				_playerAnim.SetBool("IsIdle", value: true);
				_playerAnim.SetBool("IsWalking", value: false);
				_playerAnim.SetBool("IsRunning", value: false);
			}
			if (Sitting)
			{
				curMoveSpeed = MoveSpeed / 2f;
				curSprintSpeed = SprintSpeed / 2f;
				_enemy.ViewAngle = _enemy.defaultAngle / 2f;
				_enemy.ViewRadius = _enemy.defaultRadius / 2f;
				footStepTimer = 2f;
				_audio.volume = 0.3f;
			}
			else
			{
				curMoveSpeed = MoveSpeed;
				curSprintSpeed = SprintSpeed;
				_enemy.ViewAngle = _enemy.defaultAngle;
				_enemy.ViewRadius = _enemy.defaultRadius;
				_audio.volume = 0.5f;
			}
			if (_input.sprint)
			{
				if (Sitting)
				{
					_enemy.ViewAngle = _enemy.defaultAngle;
					_enemy.ViewRadius = _enemy.defaultRadius;
					footStepTimer = 1f;
					_audio.volume = 0.6f;
				}
				else
				{
					_enemy.ViewAngle = 270f;
					_enemy.ViewRadius = 10f;
					footStepTimer = 0.5f;
					_audio.volume = 0.8f;
				}
			}
			else
			{
				_enemy.ViewAngle = _enemy.defaultAngle;
				_enemy.ViewRadius = _enemy.defaultRadius;
				footStepTimer = 1f;
				_audio.volume = 0.7f;
			}
			float num2 = (_input.sprint ? curSprintSpeed : curMoveSpeed);
			if (_input.move == Vector2.zero)
			{
				num2 = 0f;
			}
			float magnitude = new Vector3(_controller.velocity.x, 0f, _controller.velocity.z).magnitude;
			float num3 = 0.1f;
			float num4 = (_input.analogMovement ? _input.move.magnitude : 1f);
			if (magnitude < num2 - num3 || magnitude > num2 + num3)
			{
				_speed = Mathf.Lerp(magnitude, num2 * num4, Time.deltaTime * SpeedChangeRate);
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = num2;
			}
			Vector3 vector = new Vector3(_input.move.x, 0f, _input.move.y).normalized;
			if (_input.move != Vector2.zero)
			{
				isWalking = true;
				vector = base.transform.right * _input.move.x + base.transform.forward * _input.move.y;
			}
			else
			{
				isWalking = false;
			}
			_controller.Move(vector.normalized * (_speed * Time.deltaTime) + new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime);
		}

		private void JumpAndGravity()
		{
			if (Grounded)
			{
				if (isFalling)
				{
					_cameraAnim.SetTrigger("PlayerFall");
					_playerAnim.SetTrigger("IsFall");
					_audio.clip = _fallBreathClip[Random.Range(0, _fallBreathClip.Length)];
					_audio.PlayOneShot(_audio.clip);
					canMove = false;
					Invoke("OnCameraFallAnimationEvent", 1f);
					isFalling = false;
				}
				_fallTimeoutDelta = FallTimeout;
				if (_verticalVelocity < 0f)
				{
					_verticalVelocity = -2f;
				}
			}
			else
			{
				isFalling = true;
				if (_fallTimeoutDelta >= 0f)
				{
					_fallTimeoutDelta -= Time.deltaTime;
				}
				_input.jump = false;
			}
			if (_verticalVelocity < _terminalVelocity)
			{
				_verticalVelocity += Gravity * Time.deltaTime;
			}
		}

		private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f)
			{
				lfAngle += 360f;
			}
			if (lfAngle > 360f)
			{
				lfAngle -= 360f;
			}
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}

		private void OnDrawGizmosSelected()
		{
			Color color = new Color(0f, 1f, 0f, 0.35f);
			Color color2 = new Color(1f, 0f, 0f, 0.35f);
			if (Grounded)
			{
				Gizmos.color = color;
			}
			else
			{
				Gizmos.color = color2;
			}
			Gizmos.DrawSphere(new Vector3(base.transform.position.x, base.transform.position.y - GroundedOffset, base.transform.position.z), GroundedRadius);
		}

		private void SitDown()
		{
			if (Input.GetKeyDown(KeyCode.C))
			{
				Sitting = !Sitting;
				_controller.height = (Sitting ? SitHeight : StandHeight);
			}
		}

		private void FindObj()
		{
			if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hit, RayDist))
			{
				switch (hit.transform.tag)
				{
				case "Door":
					PressFImg.SetActive(value: true);
					if (Input.GetKeyDown(KeyCode.F))
					{
						hit.transform.GetComponent<DoorScript>().SwitchOpen();
						PressFImg.SetActive(value: false);
					}
					break;
				case "FenceDoor":
					PressFImg.SetActive(value: true);
					if (Input.GetKeyDown(KeyCode.F))
					{
						if (isHavingKey)
						{
							hit.transform.GetComponent<FenceDoorScript>().SwitchOpen();
						}
						else
						{
							StartCoroutine(FadeNeedKeyTxt());
							_audio.clip = FenceLocked;
							_audio.PlayOneShot(_audio.clip);
						}
						PressFImg.SetActive(value: false);
					}
					break;
				case "Candle":
					PressFImg.SetActive(value: true);
					if (Input.GetKeyDown(KeyCode.F))
					{
						if (!isHavingCandle)
						{
							isHavingCandle = true;
							_audio.PlayOneShot(candleClip);
							PressFImg.SetActive(value: false);
							hit.transform.GetComponent<Candle>().DeActiveCandle();
						}
						else
						{
							StartCoroutine(FadeHaveCandleTxt());
						}
					}
					break;
				case "MoveInfo":
					PressFImg.SetActive(value: true);
					if (Input.GetKeyDown(KeyCode.F))
					{
						PressFImg.SetActive(value: false);
						MoveInfoImg.SetActive(value: true);
					}
					break;
				case "FlashInfo":
					PressFImg.SetActive(value: true);
					if (Input.GetKeyDown(KeyCode.F))
					{
						PressFImg.SetActive(value: false);
						FlashInfoImg.SetActive(value: true);
					}
					break;
				case "RunInfo":
					PressFImg.SetActive(value: true);
					if (Input.GetKeyDown(KeyCode.F))
					{
						PressFImg.SetActive(value: false);
						RunInfoImg.SetActive(value: true);
					}
					break;
				case "CandleHolder":
					if (isHavingCandle)
					{
						PressFImg.SetActive(value: true);
						if (Input.GetKeyDown(KeyCode.F))
						{
							GameManager.Instance.IncreaseCandleCount();
							LastCandleTxt.gameObject.SetActive(value: true);
							LastCandleTxt.DOText($"Candle Left : {3 - GameManager.Instance.CandleCount}", 2.5f).SetEase(Ease.OutQuart);
							isHavingCandle = false;
							PressFImg.SetActive(value: false);
						}
					}
					break;
				case "FlashLight":
					if (!isHavingFlashLight)
					{
						PressFImg.SetActive(value: true);
						if (Input.GetKeyDown(KeyCode.F))
						{
							_audio.clip = flashClip;
							_audio.PlayOneShot(_audio.clip);
							PressFImg.SetActive(value: false);
							hit.transform.gameObject.SetActive(value: false);
							FlashLight.SetActive(value: true);
							isHavingFlashLight = true;
						}
					}
					break;
				case "FenceKey":
					PressFImg.SetActive(value: true);
					if (Input.GetKeyDown(KeyCode.F))
					{
						PressFImg.SetActive(value: false);
						hit.transform.gameObject.SetActive(value: false);
						isHavingKey = true;
						_audio.clip = FenceKeyGet;
						_audio.PlayOneShot(_audio.clip);
					}
					break;
				default:
					PressFImg.SetActive(value: false);
					break;
				}
			}
			else
			{
				PressFImg.SetActive(value: false);
			}
		}

		private void OnDrawGizmos()
		{
			if (!(_mainCamera == null))
			{
				Gizmos.color = Color.magenta;
				Gizmos.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward * RayDist);
			}
		}

		private void HeartBounce()
		{
			curTime += Time.deltaTime;
			if (curTime >= bounceTimer)
			{
				if (Vector3.Distance(_enemy.gameObject.transform.position, base.gameObject.transform.position) <= 10f)
				{
					_audio.volume = 1.2f;
					_audio.PlayOneShot(_Heartclips[1]);
				}
				else if (Vector3.Distance(_enemy.gameObject.transform.position, base.gameObject.transform.position) <= 5f)
				{
					_audio.volume = 1.2f;
					_audio.PlayOneShot(_Heartclips[0]);
				}
				curTime = 0f;
			}
		}

		public void PlayerHit()
		{
			curHP--;
			_audio.clip = DieScream;
			_audio.PlayOneShot(_audio.clip);
			if (curHP > 0)
			{
				Hearts[heartIdx].SetActive(value: true);
				heartIdx++;
			}
			if (curHP <= 0)
			{
				_audio.Stop();
				GameManager.Instance.StopAudio();
				_loadingPanel.gameObject.SetActive(value: true);
				_loadingPanel.CaughtSceneLoad();
			}
		}

		public void OnCameraFallAnimationEvent()
		{
			canMove = true;
		}

		private IEnumerator FadeHaveCandleTxt()
		{
			HaveCandleTxt.gameObject.SetActive(value: true);
			HaveCandleTxt.text = "I have to find the <color=blue>Magic Circle</color> first.";
			HaveCandleTxt.DOFade(1f, 1f);
			yield return new WaitForSeconds(1.5f);
			HaveCandleTxt.DOFade(0f, 1f);
			yield return new WaitForSeconds(1f);
			HaveCandleTxt.gameObject.SetActive(value: false);
		}

		private IEnumerator FadeNeedKeyTxt()
		{
			NeedKeyTxt.gameObject.SetActive(value: true);
			NeedKeyTxt.text = "I think I need a <color=blue> Key.</color>";
			NeedKeyTxt.DOFade(1f, 1f);
			yield return new WaitForSeconds(1.5f);
			HaveCandleTxt.DOFade(0f, 1f);
			yield return new WaitForSeconds(1f);
			NeedKeyTxt.text = "";
			HaveCandleTxt.gameObject.SetActive(value: false);
		}

		public void StartBreath()
		{
			breathTime += Time.deltaTime;
			if (breathTimer < breathTime)
			{
				_audio.clip = _BreatheClips[Random.Range(0, _BreatheClips.Length)];
				_audio.PlayOneShot(_audio.clip);
				breathTime = 0f;
			}
		}
	}
}
