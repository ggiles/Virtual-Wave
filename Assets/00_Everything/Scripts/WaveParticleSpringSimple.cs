using UnityEngine;
using System.Collections;

public class WaveParticleSpringSimple : MonoBehaviour {

	public Transform targetObject;
	public float spring = 1;
	public float damp = 1;

	void Start () {
	
	}
	
	void Update () {
	
	}

	void FixedUpdate ()
	{
		// spring physics
		Vector3 posDiff = transform.position - targetObject.position;
		Vector3 forceToAdd =  - spring * posDiff - damp * rigidbody.GetRelativePointVelocity(targetObject.position);
		rigidbody.AddForce(forceToAdd);

//		// calculate velocity
//		myVel = (transform.position - lastPos) / Time.deltaTime; 
//		lastPos = transform.position;
//
//		// calculate acceleration
//		acceleration = (myVel - lastVel) / Time.deltaTime;
//		lastVel = myVel;

	}
}
