using UnityEngine;

public class CameraFacing : MonoBehaviour
{
	public Camera cameraToLookAt;

	private void Awake()
	{
		cameraToLookAt = Camera.main;
	}

	private void Update()
	{
		Vector3 vector = cameraToLookAt.transform.position - base.transform.position;
		vector.x = (vector.z = 0f);
		base.transform.LookAt(cameraToLookAt.transform.position - vector);
	}
}
