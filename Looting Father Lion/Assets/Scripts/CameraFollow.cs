using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	// Declared variables for use in script
	// Private
	private Vector3 velocity = Vector3.zero;

	// Public
	public float dampTime = 0.15f;
	public Transform target;

	void FixedUpdate () 
	{
		// Camera dampening based on the position of a target object (camera anchor point)
		// This serves to smooth the camera movement by delaying the camera movement slightly based on velocity
		if (target)
		{
			Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
			Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}

	}
}