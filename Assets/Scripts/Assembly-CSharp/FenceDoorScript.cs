using System.Collections;
using UnityEngine;

public class FenceDoorScript : MonoBehaviour
{
	public bool isOpened;

	private Animator doorAnim;

	private AudioSource doorSound;

	public AudioClip[] doorClip;

	private int clipIdx;

	private void Start()
	{
		doorAnim = GetComponent<Animator>();
		doorSound = GetComponent<AudioSource>();
		doorSound.mute = true;
		doorSound.loop = false;
	}

	public void SwitchOpen()
	{
		doorSound.mute = false;
		isOpened = !isOpened;
		if (isOpened)
		{
			clipIdx = 0;
		}
		else
		{
			clipIdx = 1;
		}
		doorAnim.SetTrigger("DoorTrigger");
		StartCoroutine(PlayClip());
	}

	private IEnumerator PlayClip()
	{
		doorSound.PlayOneShot(doorClip[clipIdx]);
		yield return new WaitForSeconds(1f);
		doorSound.mute = true;
	}
}
