using UnityEngine;

public class EndingManager : MonoBehaviour
{
	public GameObject endingPanel;

	public LoadingPanel loadingPanel;

	private void Start()
	{
		loadingPanel = loadingPanel.GetComponent<LoadingPanel>();
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;
	}

	public void Ending()
	{
		endingPanel.SetActive(value: true);
		loadingPanel.gameObject.SetActive(value: false);
	}

	public void GoToTitle()
	{
		endingPanel.SetActive(value: false);
		loadingPanel.gameObject.SetActive(value: true);
		loadingPanel.TitleSceneLoad();
	}

	public void GoToGame()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		endingPanel.SetActive(value: false);
		loadingPanel.gameObject.SetActive(value: true);
		loadingPanel.GameSceneLoad();
	}
}
