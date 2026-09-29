using System.Collections;
using Cinemachine;
using DG.Tweening;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
	public bool isOption;

	private FirstPersonController _player;

	public GameObject PausePanel;

	public GameObject _finalRoad;

	public GameObject _finalLamps;

	public GameObject _doorCol;

	public LoadingPanel _loadingPanel;

	public TextMeshProUGUI _gameTimer;

	public GameObject _GameStartTimeLine;

	public Button skipBtn;

	public Image _centerImg;

	public GameObject _candleUIImg;

	public CinemachineVirtualCamera LastCandleVC;

	private float _gameTime;

	private bool _isGameOver;

	private void Awake()
	{
		_player = Object.FindObjectOfType<FirstPersonController>();
		_loadingPanel = _loadingPanel.GetComponent<LoadingPanel>();
	}

	private void Start()
	{
		GameManager.Instance.InitializeGameManager();
		_GameStartTimeLine.SetActive(value: true);
		_candleUIImg.SetActive(value: false);
		_centerImg.gameObject.SetActive(value: false);
		GameManager.Instance.InitializeGameManager();
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Confined;
		_finalRoad.SetActive(value: false);
		_finalLamps.SetActive(value: false);
		_doorCol.SetActive(value: true);
		skipBtn.gameObject.SetActive(value: true);
		_gameTimer.gameObject.SetActive(value: false);
		_gameTime = GameManager.Instance.GameTime;
		_isGameOver = false;
	}

	private void Update()
	{
		if (GameManager.Instance._isGameStart)
		{
			if (MenuManager.Instance == null)
			{
				PausePanel.GetComponent<OptionPanel>()._volumeSlider.value = 1f;
				PausePanel.GetComponent<OptionPanel>()._mouseSensibilitySlider.value = 1f;
			}
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				isOption = !isOption;
			}
			if (isOption)
			{
				PausePanel.SetActive(value: true);
				_player.isPause = true;
				Time.timeScale = 0f;
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.Confined;
				_centerImg.gameObject.SetActive(value: false);
				_gameTimer.gameObject.SetActive(value: false);
			}
			else
			{
				PausePanel.SetActive(value: false);
				Time.timeScale = 1f;
				_player.isPause = false;
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked;
				_centerImg.gameObject.SetActive(value: true);
				_gameTimer.gameObject.SetActive(value: true);
			}
			if (_player.curHP <= 0)
			{
				_isGameOver = true;
			}
		}
	}

	public void OnEnemyFinalPhase()
	{
		_finalRoad.SetActive(value: true);
		_finalLamps.SetActive(value: true);
		_doorCol.SetActive(value: false);
	}

	public void OnQuitButtonClickInGameScene()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		isOption = false;
	}

	public void OnGameStartTimeLineEnd()
	{
		_candleUIImg.SetActive(value: true);
		_GameStartTimeLine.SetActive(value: false);
		skipBtn.gameObject.SetActive(value: false);
		GameManager.Instance._isGameStart = true;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		_gameTimer.gameObject.SetActive(value: true);
		StartCoroutine(StartGameTimer());
	}

	public void OnTimeLineSkip()
	{
		_GameStartTimeLine.GetComponent<PlayableDirector>().Stop();
		OnGameStartTimeLineEnd();
	}

	public void StartFinalPhaseCoroutine()
	{
		StartCoroutine(OnFinalPhase());
	}

	public IEnumerator OnFinalPhase()
	{
		LastCandleVC.Priority = 101;
		_centerImg.gameObject.SetActive(value: false);
		yield return new WaitForSeconds(3f);
		LastCandleVC.Priority = 10;
		yield return new WaitForSeconds(1f);
		_centerImg.gameObject.SetActive(value: true);
		GameManager.Instance.EnemyFinalPhase();
	}

	private IEnumerator StartGameTimer()
	{
		while (!_isGameOver && _gameTime != 0f)
		{
			_gameTime--;
			int num = Mathf.FloorToInt(_gameTime / 60f);
			int num2 = Mathf.FloorToInt(_gameTime % 60f);
			if (num > 0)
			{
				if (num2 > 10)
				{
					_gameTimer.text = string.Format("<i>{0} : {1}</i>", "0" + num, num2);
				}
				else
				{
					_gameTimer.text = "<i>0" + num + " : 0" + num2 + "</i>";
				}
			}
			else
			{
				_gameTimer.text = $"<i><color=red>{num} : {num2}</color></i>";
				_gameTimer.DOGlowColor(new Color(1f, 0f, 0f), 1f).SetLoops(-1).SetEase(Ease.InCirc);
			}
			yield return new WaitForSeconds(1f);
		}
		Debug.Log("TimeOver");
		_loadingPanel.gameObject.SetActive(value: true);
		_loadingPanel.TimeEndingScene();
	}
}
