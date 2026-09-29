using UnityEngine;

public class EnemyAttackEvent : MonoBehaviour
{
	private Enemy _enemy;

	private void Start()
	{
		_enemy = Object.FindObjectOfType<Enemy>();
	}

	public void EnemyAttackFin()
	{
		_enemy.isAttack = false;
		_enemy.state = Enemy.EnemyState.Chase;
		_enemy.curAttackTime = 2f;
	}
}
