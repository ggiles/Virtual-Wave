using UnityEngine;
using System.Collections;

public class VehicleParticleSpring : MonoBehaviour {

//	public Transform targetObject;
//	[HideInInspector]
	public Transform[] targetObjects;
	public float spring = 50;
	public float damp = 5;
//	[HideInInspector]
	public float[] targetDistances;


	void Start () {
   		for (int i = 0; i < targetObjects.Length; i++)
		{
			targetDistances[i] = (transform.position - targetObjects[i].position).magnitude;
		}
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

		for (int i = 0; i < targetObjects.Length; i++)
		{
			Vector3 forceToAdd = - spring * ( posDiff.magnitude - targetDistances[i] ) * (posDiff.normalized) - (damp * relVel);
			if (!float.IsNaN(forceToAdd.x) && !float.IsNaN(forceToAdd.y) && !float.IsNaN(forceToAdd.z))
				rigidbody.AddForce(forceToAdd); 
		}

	}
}
