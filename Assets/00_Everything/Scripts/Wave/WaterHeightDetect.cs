using UnityEngine;
using System.Collections;

[RequireComponent (typeof (WaterHeightSpring))]

public class WaterHeightDetect : MonoBehaviour {

	WaterHeightSpring whs;
	public Vector3 capsuleDir;
	public float capsuleCastRadius;
	public float waterCollideForce;
	public float maxAboveWater;
	public float maxBelowWater;
	public string onWaterState;

	void Start () 
	{
		whs = transform.GetComponent<WaterHeightSpring>();
	}

	Vector3 p1;
	Vector3 p2;

	void FixedUpdate () 
	{

		 p1 = transform.position + new Vector3(0,10,0);
		 p2 = p1 + Vector3.up * 100;

		RaycastHit[] hits;

//		Debug.DrawLine(p1,p2);
		hits = Physics.CapsuleCastAll(p1,p2,capsuleCastRadius,capsuleDir);

//		Debug.Log(hits.Length);

		int i = 0;
		float avgHeight = 0;
		while (i < hits.Length) {
			RaycastHit hit = hits[i];
			// set water height on Water Height Spring
			whs.waterHeight = hit.transform.position.y;
			// push water down
			hit.rigidbody.AddForce(0,waterCollideForce,0);
			avgHeight += hit.transform.position.y;
	
			i++;
		}
		avgHeight = avgHeight / hits.Length;
//		Debug.Log (avgHeight);
		float distanceFromWater = transform.position.y - avgHeight;

		// if above water turn on the spring
		if (distanceFromWater > maxAboveWater)
		{
			onWaterState = "aboveWater";
			whs.enabled = false;
		} else if (distanceFromWater <= maxAboveWater && distanceFromWater >= maxBelowWater) 
		{
			onWaterState = "onWater";
			whs.enabled = true;
		} else if (distanceFromWater < maxBelowWater)
		{
			onWaterState = "underWater";
			whs.enabled = true;
		}
//		Debug.Log (onWaterState);
	}


}
