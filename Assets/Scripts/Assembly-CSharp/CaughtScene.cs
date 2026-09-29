using UnityEngine;

public class CaughtScene : MonoBehaviour
{
	public LoadingPanel _loadingPanel;

	private void Start()
	{
		_loadingPanel = _loadingPanel.GetComponent<LoadingPanel>();
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;
	}

	public void OnRestartButtonClicked()
	{
		_loadingPanel.gameObject.SetActive(value: true);
		_loadingPanel.GameSceneLoad();
	}

	public void OnTitleButtonClicked()
	{
		_loadingPanel.gameObject.SetActive(value: true);
		_loadingPanel.TitleSceneLoad();
	}
}
