using UnityEngine;

public class MenuManager : MonoBehaviour
{
	private static MenuManager instance;

	public float _volumeValue = 1f;

	public float _mouseSensibility = 1f;

	public int _resolutionValue;

	public bool _isFullScreen = true;

	public static MenuManager Instance
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
}
