using UnityEngine;
using System.Collections;

public class WaveParticleSpring : MonoBehaviour {

//	public Transform targetObject;
	public Transform[] targetObjects;
	public float spring = 50;
	public float damp = 5;
	public float targetDistance = 0;


	void Start () {

	}
	
	void Update () {
	
	}

	void FixedUpdate ()
	{
		Vector3 avgTargetPosSum = new Vector3(0,0,0);
		foreach (Transform target in targetObjects)
		{
			if (target != null)
				avgTargetPosSum += target.position;
		}
		Vector3 avgTargetPos = avgTargetPosSum / targetObjects.Length;
//		Debug.Log (avgTargetPos);

		Vector3 posDiff = transform.position - avgTargetPos;
		Vector3 relVel = rigidbody.GetRelativePointVelocity(avgTargetPos);
		Vector3 forceToAdd = - spring * ( posDiff.magnitude - targetDistance ) * (posDiff.normalized) - (damp * relVel);
//		Debug.Log(forceToAdd);
		if (!float.IsNaN(forceToAdd.x) && !float.IsNaN(forceToAdd.y) && !float.IsNaN(forceToAdd.z))
			rigidbody.AddForce(forceToAdd); 

	}
}
