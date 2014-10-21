using UnityEngine;
using System.Collections;

[RequireComponent (typeof (WaterHeightSpring))]

public class WaterHeightDetect : MonoBehaviour {

	WaterHeightSpring whs;
	public Vector3 capsuleDir;
	public float capsuleCastRadius;
	public float waterCollideForce;

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

		Debug.DrawLine(p1,p2);
		hits = Physics.CapsuleCastAll(p1,p2,capsuleCastRadius,capsuleDir);

		int i = 0;
		while (i < hits.Length) {
			RaycastHit hit = hits[i];
//			Debug.Log (hit.transform.position.y);
			whs.waterHeight = hit.transform.position.y;
			// push water down
			hit.rigidbody.AddForce(0,waterCollideForce,0);
	
			i++;
		}
//		if (Physics.Raycast(
	}


}
