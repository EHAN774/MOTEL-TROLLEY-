using UnityEngine;

public class FinalLamp : MonoBehaviour
{
	[SerializeField]
	private GameObject leftLight;

	[SerializeField]
	private GameObject rightLight;

	[SerializeField]
	private bool isFinalLamp;

	private GameObject playerObj;

	private Enemy enemy;

	private bool isOn;

	private void Start()
	{
		isFinalLamp = false;
		playerObj = GameObject.FindWithTag("Player");
		enemy = Object.FindObjectOfType<Enemy>();
		SwitchLamp(turn: false);
	}

	public void SwitchLamp(bool turn)
	{
		if (turn)
		{
			isOn = true;
			leftLight.SetActive(value: true);
			rightLight.SetActive(value: true);
			leftLight.GetComponent<Light>().enabled = true;
			rightLight.GetComponent<Light>().enabled = true;
		}
		else
		{
			isOn = false;
			leftLight.SetActive(value: false);
			rightLight.SetActive(value: false);
			leftLight.GetComponent<Light>().enabled = false;
			rightLight.GetComponent<Light>().enabled = false;
		}
	}

	private void Update()
	{
		if (Vector3.Distance(base.transform.position, playerObj.transform.position) <= 10f && !isOn)
		{
			SwitchLamp(turn: true);
		}
		if (enemy.isFinalPhase && isFinalLamp)
		{
			SwitchLamp(turn: true);
		}
	}
}
