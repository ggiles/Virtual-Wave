using UnityEngine;
using System.Collections;

public class VehicleMotor : MonoBehaviour {

	public float speed;
	public float torque;

	void Start () 
	{
		rigidbody.centerOfMass = new Vector3(0,0,0);
	}
	
	void Update () 
	{

	}

	void FixedUpdate ()
	{
		Vector3 forceToAdd = new Vector3(0,0,Input.GetAxis("Vertical")*speed);
		rigidbody.AddRelativeForce(forceToAdd);
		Vector3 torqueToAdd = new Vector3(0,Input.GetAxis("Horizontal") * torque,0);
		rigidbody.AddRelativeTorque( torqueToAdd);
	}
}
