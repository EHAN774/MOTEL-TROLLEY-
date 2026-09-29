using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public AudioClip[] _jumpScareClips;

	private AudioSource _audio;

	[SerializeField]
	private bool readyJumpScare;

	[SerializeField]
	private float jumpScareTimer;

	[SerializeField]
	private float curTime;

	[SerializeField]
	private float timerSetTimer = 60f;

	[SerializeField]
	private float curTimerSetTime;

	private void Start()
	{
		_audio = GetComponent<AudioSource>();
		readyJumpScare = false;
	}

	private void Update()
	{
		curTimerSetTime += Time.deltaTime;
		if (curTimerSetTime >= timerSetTimer)
		{
			jumpScareTimer = Random.Range(5f, 10f);
			readyJumpScare = true;
			curTimerSetTime = 0f;
		}
		if (readyJumpScare)
		{
			curTime += Time.deltaTime;
			if (curTime >= jumpScareTimer)
			{
				_audio.clip = _jumpScareClips[Random.Range(0, _jumpScareClips.Length)];
				_audio.PlayOneShot(_audio.clip);
				curTime = 0f;
				readyJumpScare = false;
			}
		}
	}
}
