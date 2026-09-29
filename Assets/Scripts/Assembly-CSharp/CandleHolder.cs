using UnityEngine;

public class CandleHolder : MonoBehaviour
{
	public GameObject[] CandleGetParticles;

	public GameObject[] Candles;

	public GameObject lastParticle;

	private int curCandleCount;

	private void Start()
	{
		curCandleCount = 0;
		GameObject[] candleGetParticles = CandleGetParticles;
		for (int i = 0; i < candleGetParticles.Length; i++)
		{
			candleGetParticles[i].SetActive(value: false);
		}
		candleGetParticles = Candles;
		for (int i = 0; i < candleGetParticles.Length; i++)
		{
			candleGetParticles[i].SetActive(value: false);
		}
		lastParticle.SetActive(value: false);
	}

	public void GetCandle()
	{
		GameObject[] candleGetParticles = CandleGetParticles;
		for (int i = 0; i < candleGetParticles.Length; i++)
		{
			candleGetParticles[i].SetActive(value: true);
		}
		Candles[curCandleCount++].SetActive(value: true);
		Invoke("OffCandleEffect", 2f);
	}

	public void OffCandleEffect()
	{
		GameObject[] candleGetParticles = CandleGetParticles;
		for (int i = 0; i < candleGetParticles.Length; i++)
		{
			candleGetParticles[i].SetActive(value: false);
		}
	}

	public void GetLastCandle()
	{
		GameObject[] candleGetParticles = CandleGetParticles;
		for (int i = 0; i < candleGetParticles.Length; i++)
		{
			candleGetParticles[i].SetActive(value: true);
		}
		candleGetParticles = Candles;
		for (int i = 0; i < candleGetParticles.Length; i++)
		{
			candleGetParticles[i].SetActive(value: true);
		}
		lastParticle.SetActive(value: true);
		Invoke("TurnOffAllCandle", 2f);
	}

	public void TurnOffAllCandle()
	{
		GameObject[] candleGetParticles = CandleGetParticles;
		for (int i = 0; i < candleGetParticles.Length; i++)
		{
			candleGetParticles[i].SetActive(value: false);
		}
		candleGetParticles = Candles;
		for (int i = 0; i < candleGetParticles.Length; i++)
		{
			candleGetParticles[i].SetActive(value: false);
		}
		lastParticle.SetActive(value: false);
	}
}
