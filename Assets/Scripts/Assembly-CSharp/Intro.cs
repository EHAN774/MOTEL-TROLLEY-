using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
	public Scrollbar scrollbar;

	public Animator animator;

	public LoadingPanel loadingPanel;

	public GameObject introPanel;

	public TextMeshProUGUI introText;

	public PlayableDirector director;

	private AudioSource _audio;

	private void Start()
	{
		animator = GetComponent<Animator>();
		_audio = GetComponent<AudioSource>();
		scrollbar.onValueChanged.AddListener(GetScrollValue);
		introText.gameObject.SetActive(value: false);
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;
		_audio.PlayOneShot(_audio.clip);
	}

	public void GetScrollValue(float value)
	{
		if (value == 1f)
		{
			animator.SetTrigger("NextAnim");
		}
	}

	public void IntroText()
	{
		director.Play();
		introText.gameObject.SetActive(value: true);
		introText.DOText("Dear My Friend. \t\t\n\nFew days ago I dropped by a motel on valley.\nIn here, I found a strong ghost that i can't handle. \nI must be die, but I believe that you can seal the ghost. \n<color=red> Remember </color> : You have to seal the ghost <color=red> in 5 minutes.</color> \nSee you in the next Life. \nGood bye.\n\n\t\t\t\tFrom your best friend, Bob", 10f);
		StartCoroutine(LoadGame());
	}

	public void OnSkipBtnClicked()
	{
		StartCoroutine(SkipIntroLoadGame());
	}

	private IEnumerator LoadGame()
	{
		yield return new WaitForSeconds(10f);
		director.Stop();
		yield return new WaitForSeconds(3f);
		introPanel.SetActive(value: false);
		scrollbar.gameObject.SetActive(value: false);
		introText.gameObject.SetActive(value: false);
		loadingPanel.gameObject.SetActive(value: true);
		loadingPanel.GameSceneLoad();
	}

	private IEnumerator SkipIntroLoadGame()
	{
		director.Stop();
		yield return new WaitForSeconds(1f);
		introPanel.SetActive(value: false);
		scrollbar.gameObject.SetActive(value: false);
		introText.gameObject.SetActive(value: false);
		loadingPanel.gameObject.SetActive(value: true);
		loadingPanel.GameSceneLoad();
	}
}
