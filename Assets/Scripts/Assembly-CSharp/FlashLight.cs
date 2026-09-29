using System.Collections;
using StarterAssets;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
	private FirstPersonController controller;

	private Light _light;

	private float LightTimeOut = 0.5f;

	[Tooltip("0 : turn On, 1 : turn Off")]
	[SerializeField]
	private AudioClip[] flashSound;

	private AudioSource _audio;

	private void Start()
	{
		controller = Object.FindObjectOfType<FirstPersonController>();
		_audio = GetComponent<AudioSource>();
		_light = GetComponent<Light>();
		_light.enabled = false;
	}

	private void Update()
	{
		RotateLight();
		if (Input.GetKeyDown(KeyCode.E))
		{
			StartCoroutine(LightTurnOff());
		}
	}

	private IEnumerator LightTurnOff()
	{
		yield return new WaitForSeconds(LightTimeOut);
		_light.enabled = !_light.enabled;
		if (_light.enabled)
		{
			_audio.clip = flashSound[0];
			_audio.PlayOneShot(_audio.clip);
		}
		else
		{
			_audio.clip = flashSound[1];
			_audio.PlayOneShot(_audio.clip);
		}
		StopCoroutine(LightTurnOff());
	}

	private void RotateLight()
	{
		base.transform.parent.gameObject.transform.localRotation = Quaternion.Euler(0f, 90f, controller._cinemachineTargetPitch + 90f);
	}
}
