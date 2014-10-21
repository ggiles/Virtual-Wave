using UnityEngine;
using System.Collections;

public class AverageHeightSpring : MonoBehaviour {

	public Vector3 avgPos;
	public float spring = 1;
	public float damp = 1;
	public VehicleParticleSpring[] children;
	void Start () {

	}
	
	void Update () {

	}

	void FixedUpdate ()
	{
		// get the average height of all the vehicle particles
		Vector3 posSum = new Vector3(0,0,0);
		if ( transform.childCount > 0 )
		{
			children = transform.GetComponentsInChildren<VehicleParticleSpring>();

			foreach(VehicleParticleSpring child in children )
			{
				posSum += child.transform.position;
			}
			avgPos = posSum / children.Length; 

			// spring physics
			Vector3 posDiff = transform.position - avgPos;
			Vector3 forceToAdd =  - spring * posDiff - damp * rigidbody.GetRelativePointVelocity(avgPos);
			rigidbody.AddForce(new Vector3(0,forceToAdd.y,0));

		}



	}
}
