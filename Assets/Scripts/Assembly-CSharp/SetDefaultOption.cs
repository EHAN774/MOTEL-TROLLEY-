using UnityEngine;

public class SetDefaultOption : MonoBehaviour
{
	private void Awake()
	{
		MenuManager.Instance._isFullScreen = true;
		MenuManager.Instance._volumeValue = 1f;
		MenuManager.Instance._mouseSensibility = 1f;
		MenuManager.Instance._resolutionValue = 0;
		Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
		Screen.SetResolution(2560, 1440, fullscreen: true);
	}
}
