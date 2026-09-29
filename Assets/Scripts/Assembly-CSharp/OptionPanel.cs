using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
	[SerializeField]
	private bool isFullScreen = true;

	[SerializeField]
	private TMP_Dropdown _resolutionDropDown;

	[SerializeField]
	private Button _fullScreenModeButton;

	[SerializeField]
	public Slider _volumeSlider;

	[SerializeField]
	public Slider _mouseSensibilitySlider;

	[SerializeField]
	private GameObject _mouseSensibility;

	[SerializeField]
	private GameObject _fullScreenModeCheckImg;

	private void Start()
	{
		_resolutionDropDown.onValueChanged.AddListener(ResolutionSet);
		_volumeSlider.onValueChanged.AddListener(VolumeChange);
		_mouseSensibilitySlider.onValueChanged.AddListener(MouseSensitivityChange);
		_fullScreenModeCheckImg.SetActive(value: true);
		if (MenuManager.Instance != null)
		{
			isFullScreen = MenuManager.Instance._isFullScreen;
			_volumeSlider.value = MenuManager.Instance._volumeValue;
			_mouseSensibilitySlider.value = MenuManager.Instance._mouseSensibility;
			_resolutionDropDown.value = MenuManager.Instance._resolutionValue;
			if (MenuManager.Instance._isFullScreen)
			{
				_resolutionDropDown.interactable = false;
			}
			else
			{
				_resolutionDropDown.interactable = true;
			}
		}
		_fullScreenModeCheckImg.SetActive(isFullScreen);
	}

	public void ScreenModeButton()
	{
		isFullScreen = !isFullScreen;
		Screen.fullScreenMode = (isFullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed);
		MenuManager.Instance._isFullScreen = isFullScreen;
		_resolutionDropDown.interactable = !isFullScreen;
		_fullScreenModeCheckImg.SetActive(isFullScreen);
	}

	public void ResolutionSet(int idx)
	{
		MenuManager.Instance._resolutionValue = idx;
		switch (idx)
		{
		case 0:
			Debug.Log("2560,1440");
			Screen.SetResolution(2560, 1440, fullscreen: false);
			break;
		case 1:
			Debug.Log("1920,1080");
			Screen.SetResolution(1920, 1080, fullscreen: false);
			break;
		case 2:
			Debug.Log("1600,900");
			Screen.SetResolution(1600, 900, fullscreen: false);
			break;
		default:
			Screen.SetResolution(2560, 1440, fullscreen: false);
			break;
		}
	}

	public void OnQuitButtonClick()
	{
		base.gameObject.SetActive(value: false);
	}

	public void VolumeChange(float value)
	{
		AudioListener.volume = value;
		MenuManager.Instance._volumeValue = value;
	}

	public void MouseSensitivityChange(float value)
	{
		MenuManager.Instance._mouseSensibility = value;
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
