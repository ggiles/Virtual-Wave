using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour {

	public Transform jetSki; 

	public Transform[] cubes;

	public VehicleParticlePhysics motorCubeL;
	public VehicleParticlePhysics motorCubeR;

	public VehicleParticlePhysics axisCubeX;
	public VehicleParticlePhysics axisCubeY;
	public VehicleParticlePhysics axisCubeZ;
	public VehicleParticlePhysics axisCubeC;

	float propForce = 0.02f;
	float turnForce = 0.005f;


	void Start ()
	{

	}
	

	void FixedUpdate ()
	{
		// get center of all cubes and set the jetski model to that position
		Vector3 avgCubePos = new Vector3 (0,0,0);
		int numCubes=0;
		cubes = transform.GetComponentsInChildren<Transform>();
		foreach (Transform cube in cubes) {
			if (cube.gameObject.tag == "VehicleParticle")
			{
				avgCubePos += cube.position;
				numCubes++;
			}
		}
		avgCubePos /= numCubes;
		jetSki.position = avgCubePos;

		Vector3 xAxis = (axisCubeX.transform.position - axisCubeC.transform.position).normalized;
		//Vector3 yAxis = (axisCubeY.transform.position - axisCubeC.transform.position).normalized;
		Vector3 zAxis = (axisCubeZ.transform.position - axisCubeC.transform.position).normalized;

		jetSki.forward = zAxis;
		jetSki.Rotate (Vector3.forward, -Mathf.Rad2Deg * Vector3.Dot (xAxis, Vector3.up));



		float vInput = Input.GetAxis("Vertical") * propForce;
		motorCubeL.velocity += vInput * zAxis;
		motorCubeR.velocity += vInput * zAxis;

		float hInput = Input.GetAxis("Horizontal") * turnForce;
		motorCubeL.velocity += hInput * xAxis;
		motorCubeR.velocity += hInput * xAxis;


	}


}
