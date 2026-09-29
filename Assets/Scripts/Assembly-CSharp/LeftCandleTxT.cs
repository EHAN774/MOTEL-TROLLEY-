using TMPro;
using UnityEngine;

public class LeftCandleTxT : MonoBehaviour
{
	private TextMeshProUGUI thisTxt;

	private void Start()
	{
		thisTxt = GetComponent<TextMeshProUGUI>();
	}

	public void DeActiveLeftCandleTxt()
	{
		thisTxt.text = "";
		base.gameObject.SetActive(value: false);
	}
}
