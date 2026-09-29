using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;

	public int CandleCount;

	public readonly int MaxHP = 3;

	[SerializeField]
	public float GameTime = 300f;

	public AudioClip candleClip;

	public AudioClip finalPhaseClip;

	public bool _candleGet;

	public bool _lastCandleGet;

	public bool _isGameStart;

	private Enemy _enemy;

	[SerializeField]
	private GameScene _gameScene;

	private AudioSource _audio;

	[SerializeField]
	private CandleHolder _candleHolder;

	public static GameManager Instance
	{
		get
		{
			if (instance == null)
			{
				return null;
			}
			return instance;
		}
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void Start()
	{
		_audio = GetComponent<AudioSource>();
		InitializeGameManager();
	}

	public void IncreaseCandleCount()
	{
		if (0 <= CandleCount && CandleCount < 2)
		{
			CandleCount++;
			_audio.clip = candleClip;
			_audio.PlayOneShot(_audio.clip);
			_candleHolder.GetCandle();
		}
		else if (CandleCount >= 2)
		{
			CandleCount++;
			_gameScene.StartFinalPhaseCoroutine();
			_gameScene.OnEnemyFinalPhase();
			_audio.clip = finalPhaseClip;
			_audio.Play();
		}
	}

	public void EnemyFinalPhase()
	{
		Debug.Log("Enemy Final Phase");
		_enemy.isFinalPhase = true;
		_enemy.state = Enemy.EnemyState.FinalPhase;
	}

	public void InitializeGameManager()
	{
		if (SceneManager.GetActiveScene().buildIndex == 2)
		{
			_enemy = Object.FindObjectOfType<Enemy>();
			_candleHolder = Object.FindObjectOfType<CandleHolder>();
			_gameScene = Object.FindObjectOfType<GameScene>();
		}
		CandleCount = 0;
		_candleGet = false;
		_lastCandleGet = false;
		_isGameStart = false;
	}

	public void StopAudio()
	{
		_audio.Stop();
	}
}
