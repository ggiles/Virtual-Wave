using UnityEngine;
using System.Collections;

public class VehicleMotor : MonoBehaviour {

	void Start () 
	{
		rigidbody.centerOfMass = new Vector3(0,-10,0);
	}
	
	void Update () 
	{
		Debug.DrawRay(rigidbody.centerOfMass,Vector3.down);
	}
}
