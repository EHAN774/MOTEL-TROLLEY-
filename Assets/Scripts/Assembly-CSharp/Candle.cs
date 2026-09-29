using UnityEngine;

public class Candle : MonoBehaviour
{
	[SerializeField]
	private SpawnManager spawnManager;

	public int thisIdx;

	private void Start()
	{
	}

	public void DeActiveCandle()
	{
		base.gameObject.SetActive(value: false);
	}
}
