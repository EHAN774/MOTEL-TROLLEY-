using UnityEngine;
using UnityEngine.Playables;

public class TimeEnding : MonoBehaviour
{
	[SerializeField]
	private GameObject _endingPanel;

	[SerializeField]
	private LoadingPanel _loadingPanel;

	private PlayableDirector _playingDirector;

	private void Start()
	{
		_loadingPanel = _loadingPanel.GetComponent<LoadingPanel>();
		_playingDirector = GetComponent<PlayableDirector>();
		_endingPanel.SetActive(value: false);
		_loadingPanel.gameObject.SetActive(value: false);
	}

	public void OnTimeLineEnded()
	{
		_endingPanel.gameObject.SetActive(value: true);
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;
	}

	public void OnRestartBtnClicked()
	{
		_endingPanel.gameObject.SetActive(value: false);
		_loadingPanel.gameObject.SetActive(value: true);
		_loadingPanel.GameSceneLoad();
	}

	public void OnGoTitleBtnClicked()
	{
		_endingPanel.gameObject.SetActive(value: false);
		_loadingPanel.gameObject.SetActive(value: true);
		_loadingPanel.TitleSceneLoad();
	}
}
