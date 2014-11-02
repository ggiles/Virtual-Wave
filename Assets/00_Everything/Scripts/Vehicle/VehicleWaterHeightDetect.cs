using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VehicleWaterHeightDetect : MonoBehaviour {

	List<Transform> closeWater = new List<Transform>();

	void Start () 
	{
	
	}
	
	void FixedUpdate () 
	{
		Debug.Log (closeWater.Count);
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Water")
		{
			closeWater.Add(collider.transform);
		}
	
	}
	
	void OnTriggerExit(Collider collider)
	{
		if (collider.tag == "Water")
		{
			closeWater.Remove(collider.transform);
		}
	}
}
