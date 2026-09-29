using UnityEngine;

public class EnemySounds : MonoBehaviour
{
	public AudioClip[] EnemySoundClips;

	public AudioClip[] attackSounds;

	private AudioSource EnemySound;

	private Enemy.EnemyState enemyState;

	private CharacterController _player;

	private int ran;

	private float footstepTimer = 10f;

	private float curTimer;

	private float ObserveDistance = 10f;

	private void Start()
	{
		EnemySound = GetComponent<AudioSource>();
		_player = Object.FindObjectOfType<CharacterController>();
	}

	private void Update()
	{
		if (Vector3.Distance(base.gameObject.transform.position, _player.gameObject.transform.position) <= ObserveDistance)
		{
			curTimer += Time.deltaTime;
			if (enemyState != Enemy.EnemyState.FinalPhase && curTimer >= footstepTimer)
			{
				ran = Random.Range(0, EnemySoundClips.Length);
				EnemySound.clip = EnemySoundClips[ran];
				EnemySound.PlayOneShot(EnemySound.clip);
				curTimer = 0f;
			}
		}
	}

	public void PlayAttackSound()
	{
		int num = Random.Range(0, attackSounds.Length);
		EnemySound.clip = attackSounds[num];
		EnemySound.PlayOneShot(EnemySound.clip);
	}
}
