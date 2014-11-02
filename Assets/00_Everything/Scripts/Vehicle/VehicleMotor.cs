using UnityEngine;
using System.Collections;

[RequireComponent (typeof (WaterHeightDetect))]


public class VehicleMotor : MonoBehaviour {

	public float speed;
	public float torque;
	public float fallSpeed;

	private WaterHeightDetect whd;

	void Start () 
	{
		rigidbody.centerOfMass = new Vector3(0,0,0);
		whd = transform.GetComponent<WaterHeightDetect>();
	}
	
	void Update () 
	{

	}



	void FixedUpdate ()
	{
		if (whd.onWaterState == "onWater")
		{
			Vector3 forceToAdd = new Vector3(0,0,Input.GetAxis("Vertical")*speed);
			if (Input.GetButtonDown("Jump") == true)
				forceToAdd += new Vector3(0,speed * 25,0);
			rigidbody.AddRelativeForce(forceToAdd);
			Vector3 torqueToAdd = new Vector3(0,Input.GetAxis("Horizontal") * torque,0);
			rigidbody.AddRelativeTorque( torqueToAdd);
		}
		if (whd.onWaterState == "aboveWater")
		{
			Vector3 forceToAdd = new Vector3(0,-fallSpeed,0);
			rigidbody.AddForce(forceToAdd);

		}
	}
}
