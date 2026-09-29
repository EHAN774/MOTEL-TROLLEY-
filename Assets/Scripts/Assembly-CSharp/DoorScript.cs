using System.Collections;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
	public bool isOpened;

	[SerializeField]
	private bool canOpen;

	private Animator doorAnim;

	private AudioSource doorSound;

	public AudioClip[] doorClip;

	[SerializeField]
	private AudioClip lockedClip;

	private int clipIdx;

	private void Start()
	{
		doorAnim = GetComponent<Animator>();
		doorSound = GetComponent<AudioSource>();
		doorSound.loop = false;
	}

	public void SwitchOpen()
	{
		if (canOpen)
		{
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
			StartCoroutine(PlayClip(doorClip[clipIdx]));
		}
		else
		{
			StartCoroutine(PlayClip(lockedClip));
		}
	}

	private IEnumerator PlayClip(AudioClip _clip)
	{
		doorSound.PlayOneShot(_clip);
		yield return null;
	}
}
