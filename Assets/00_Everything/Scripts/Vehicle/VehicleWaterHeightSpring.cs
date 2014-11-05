using UnityEngine;
using System.Collections;

public class VehicleWaterHeightSpring : MonoBehaviour {

	public float upForce; 

	void Start () 
	{
	
	}
	
	void FixedUpdate () 
	{
		Transform vehicle = transform.parent;
		int layerMask = 1 << 4; // this makes it only collide with objects in layer 4 (Water)
		RaycastHit hit;
		if (Physics.Raycast(transform.position, -Vector3.up, out hit, 1000.0F,layerMask)) // cast down
		{
			Debug.DrawLine(transform.position, hit.point,Color.green);
			float distanceToWater = hit.distance;
			Debug.Log("aboveWater");
			Debug.Log(distanceToWater);
		} else if (Physics.Raycast(transform.position, Vector3.up, out hit, 1000.0F,layerMask)) { // cast up
			Debug.DrawLine(transform.position, hit.point,Color.green);
			float distanceToWater = hit.distance;
			Debug.Log("belowWater");
			Debug.Log(distanceToWater);
		}

//		if (transform.position.y <= 0) // if underwater
//		{
//			Debug.Log("add force up");
//			Vector3 force = new Vector3(0,upForce,0);
//			vehicle.rigidbody.AddForceAtPosition(force, transform.position); // add force at my position
//		}
	}
}
