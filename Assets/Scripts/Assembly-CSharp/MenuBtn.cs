using UnityEngine;

public class MenuBtn : MonoBehaviour
{
	private LoadingPanel LoadPanel;

	private OptionPanel OptionPanel;

	public AudioClip btnClip;

	private AudioSource _audio;

	private void Start()
	{
		LoadPanel = Object.FindObjectOfType<LoadingPanel>();
		OptionPanel = Object.FindObjectOfType<OptionPanel>();
		_audio = GetComponent<AudioSource>();
		LoadPanel.gameObject.SetActive(value: false);
		OptionPanel.gameObject.SetActive(value: false);
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Confined;
	}

	public void OnStartBtnClick()
	{
		Debug.Log("GameStart");
		LoadPanel.gameObject.SetActive(value: true);
		LoadPanel.IntroSceneLoad();
		_audio.PlayOneShot(btnClip);
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void OnOptionBtnClick()
	{
		Debug.Log("SetOption");
		_audio.PlayOneShot(btnClip);
		OptionPanel.gameObject.SetActive(value: true);
	}
}
