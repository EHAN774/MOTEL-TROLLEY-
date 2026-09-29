using UnityEngine;

[ExecuteInEditMode]
public class SamplesShowcase : MonoBehaviour
{
	public string headline = "Headline Goes Here";

	public Color headlineColor = Color.white;

	public Color linkColor = Color.white;

	public TextAsset SamplesDescriptions;

	public GameObject[] samplesPrefabs;

	public int currentIndex;

	private Object currentPrefab;

	private int prefabIndex;

	public GameObject instantiatedPrefab;

	private bool needUpdate;

	private void Start()
	{
	}

	private void OnValidate()
	{
		needUpdate = true;
	}

	private void Update()
	{
		if (Application.isFocused && Input.GetKeyDown(KeyCode.Space))
		{
			SwitchEffect();
		}
		if (needUpdate && currentIndex != prefabIndex)
		{
			CleanChildren();
			InstantiateSample(currentIndex);
			needUpdate = false;
		}
	}

	private void SwitchEffect()
	{
		currentIndex++;
		currentIndex = ((currentIndex <= samplesPrefabs.Length - 1) ? currentIndex : 0);
		needUpdate = true;
	}

	private void InstantiateSample(int index)
	{
		if (currentIndex <= samplesPrefabs.Length && samplesPrefabs.Length != 0)
		{
			currentPrefab = samplesPrefabs[index];
		}
		if (currentPrefab != null)
		{
			instantiatedPrefab = Object.Instantiate(currentPrefab, base.transform.position, Quaternion.identity) as GameObject;
			instantiatedPrefab.transform.parent = base.gameObject.transform;
			prefabIndex = currentIndex;
		}
	}

	private void CleanChildren()
	{
		if (base.transform.childCount <= 0)
		{
			return;
		}
		foreach (Transform item in base.transform)
		{
			Object.DestroyImmediate(item.gameObject);
		}
	}
}
