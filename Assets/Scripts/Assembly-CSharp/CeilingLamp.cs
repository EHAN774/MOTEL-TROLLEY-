using System.Collections;
using UnityEngine;

public class CeilingLamp : MonoBehaviour
{
	private CharacterController _player;

	private Light _light;

	public bool isAuto;

	public bool isOn;

	[SerializeField]
	private float _distance = 3f;

	private AudioSource _audio;

	[Space(5f)]
	[SerializeField]
	private bool isBlank;

	[SerializeField]
	private bool isBlankOnce;

	[SerializeField]
	private AudioClip _blanckClip;

	[Space(5f)]
	[SerializeField]
	private bool isJumpScare;

	[SerializeField]
	private bool isJumpScareOnce;

	[SerializeField]
	private AudioClip[] JumpScareSoundsOnLight;

	private void Start()
	{
		_player = Object.FindObjectOfType<CharacterController>();
		_light = GetComponent<Light>();
		_audio = GetComponent<AudioSource>();
		_light.enabled = false;
		isOn = false;
	}

	private void Update()
	{
		if (!(_player.transform.position.y < base.transform.position.y))
		{
			return;
		}
		if (isAuto)
		{
			if (Vector3.Distance(base.transform.position, _player.transform.position) <= _distance)
			{
				if (isOn)
				{
					return;
				}
				_light.enabled = true;
				isOn = true;
				if (isBlank)
				{
					StartCoroutine(BlankLight());
				}
				if (isJumpScare)
				{
					_audio.clip = JumpScareSoundsOnLight[Random.Range(0, JumpScareSoundsOnLight.Length)];
					_audio.PlayOneShot(_audio.clip);
					if (isJumpScareOnce)
					{
						isJumpScare = false;
					}
				}
			}
			else if (isOn)
			{
				isOn = false;
				_light.enabled = false;
			}
		}
		else if (!isOn)
		{
			isOn = true;
			_light.enabled = true;
		}
	}

	private IEnumerator BlankLight()
	{
		float _time = 2f;
		_audio.PlayOneShot(_blanckClip, 0.7f);
		while (_time > 0f)
		{
			_time -= Time.deltaTime;
			_light.enabled = !_light.enabled;
			yield return new WaitForEndOfFrame();
		}
		if (isBlankOnce)
		{
			isBlank = false;
		}
	}
}
