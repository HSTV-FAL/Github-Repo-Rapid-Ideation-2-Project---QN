using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	public Vector3 offset = new Vector3(-4, 4, -8);
	public float smoothSpeed = 5f;

	void LateUpdate()
	{
		if (target == null) return;

		Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
		
		transform.position =  smoothedPosition;

		transform.rotation = Quaternion.Euler(25f, 0f, 0f);
	}
}
