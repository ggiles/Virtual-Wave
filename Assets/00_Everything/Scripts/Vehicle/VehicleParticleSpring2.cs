using UnityEngine;
using System.Collections;

public class VehicleParticleSpring2 : MonoBehaviour {

	public Transform targetObject;
	public float spring = 5;
	public float damp = 1;
	public float targetDistance = 3;

	public float currentDistance;
	public Vector3 targetNormal;
	public Vector3 myPreviousPos;
	public Vector3 myVelocity;
	public Vector3 targetPreviousPos;
	public Vector3 targetVelocity;
	public Vector3 relativeVelocity;

	void Start ()
	{
		myPreviousPos = new Vector3(0,0,0);
		targetPreviousPos = new Vector3(0,0,0);
	}
	
	void FixedUpdate () 
	{
		// calculate velocity of me and targetObject 
		myVelocity = (transform.position - myPreviousPos) / Time.deltaTime;
		myPreviousPos = transform.position;
		targetVelocity = (targetObject.position - targetPreviousPos) / Time.deltaTime;
		targetPreviousPos = targetObject.position;

		targetNormal = (transform.position - targetObject.position).normalized;
		currentDistance = (transform.position - targetObject.position).magnitude;
		relativeVelocity = myVelocity - targetVelocity;
		Vector3 forceToAdd = - spring * (targetDistance - currentDistance) * targetNormal - damp * relativeVelocity;

		Debug.Log ("force to add: " + forceToAdd);


	}
}
