using UnityEngine;

public class EndingTriggerCollider : MonoBehaviour
{
	[SerializeField]
	private CharacterController _player;

	[SerializeField]
	private LoadingPanel _loadingPanel;

	[SerializeField]
	private float distance;

	private void Start()
	{
		_player = Object.FindObjectOfType<CharacterController>();
		_loadingPanel = _loadingPanel.GetComponent<LoadingPanel>();
	}

	private void Update()
	{
		distance = Vector3.Distance(base.gameObject.transform.position, _player.gameObject.transform.position);
		if (distance < 5f)
		{
			_loadingPanel.gameObject.SetActive(value: true);
			_loadingPanel.EndingSceneLoad();
			GameManager.Instance._isGameStart = false;
			GameManager.Instance.StopAudio();
		}
	}
}
