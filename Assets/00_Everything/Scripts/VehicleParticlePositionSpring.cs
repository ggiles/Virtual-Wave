using UnityEngine;
using System.Collections;

public class VehicleParticlePositionSpring : MonoBehaviour {

	[HideInInspector]
	public GameObject targetObject; 
	private Vector3 targetPosition;
	public float spring = 1;
	public float damp = 1;

	void Start () {
		// set the target position to the initial position
		targetPosition = targetObject.transform.position;
	}
	
	void Update () {
		targetPosition = targetObject.transform.position;
	}

	void FixedUpdate ()
	{
		// spring physics
		Vector3 posDiffSpring = transform.position - targetPosition;
		Vector3 forceToAdd =  - spring * posDiffSpring - damp * rigidbody.GetRelativePointVelocity(targetPosition);
		rigidbody.AddForce(forceToAdd);

	}
}
