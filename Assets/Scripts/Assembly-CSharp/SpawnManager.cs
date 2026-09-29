using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public GameObject[] items;

	public Transform[] spawnPoints;

	public GameObject candlePrefab;

	private List<int> GotItemIDXs;

	public int totalSpawnNum;

	[SerializeField]
	private int MaxCandleSpawn = 3;

	private void Awake()
	{
		GotItemIDXs = new List<int>();
		totalSpawnNum = spawnPoints.Length;
		items = new GameObject[totalSpawnNum];
	}

	private void Start()
	{
		Generate();
		ActiveItem();
	}

	private void Generate()
	{
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = Object.Instantiate(candlePrefab, spawnPoints[i].position, spawnPoints[i].rotation);
			items[i].SetActive(value: false);
			items[i].GetComponent<Candle>().thisIdx = i;
		}
	}

	public void ActiveObj(int idx)
	{
		if (items[idx] != null)
		{
			items[idx].SetActive(value: true);
		}
	}

	public void ActiveItem()
	{
		int num = Random.Range(0, totalSpawnNum);
		for (int i = 0; i < MaxCandleSpawn; i++)
		{
			GotItemIDXs.Add(num);
			ActiveObj(num);
			while (GotItemIDXs.Contains(num))
			{
				num = Random.Range(0, totalSpawnNum);
			}
		}
	}

	public void ActiveNextItem(int gotItemIDX)
	{
		GotItemIDXs.Add(gotItemIDX);
		if (GotItemIDXs.Count != MaxCandleSpawn)
		{
			int num = FindNewCandle(gotItemIDX);
			ActiveObj(num);
			items[num].transform.position = spawnPoints[num].position;
			items[num].transform.rotation = spawnPoints[num].rotation;
		}
	}

	private int FindNewCandle(int exception)
	{
		int num = Random.Range(0, totalSpawnNum);
		while (GotItemIDXs.Contains(num))
		{
			num = Random.Range(0, totalSpawnNum);
		}
		return num;
	}
}
