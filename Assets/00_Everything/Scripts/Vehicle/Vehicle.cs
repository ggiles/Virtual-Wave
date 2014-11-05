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

	float propForce = 0.01f;
	float turnForce = 0.005f;


	void Start ()
	{

	}

	/*
	public static Quaternion QuaternionFromMatrix(Matrix4x4 m) {
		// Adapted from: http://www.euclideanspace.com/maths/geometry/rotations/conversions/matrixToQuaternion/index.htm
		Quaternion q = new Quaternion();
		q.w = Mathf.Sqrt( Mathf.Max( 0, 1 + m[0,0] + m[1,1] + m[2,2] ) ) / 2; 
		q.x = Mathf.Sqrt( Mathf.Max( 0, 1 + m[0,0] - m[1,1] - m[2,2] ) ) / 2; 
		q.y = Mathf.Sqrt( Mathf.Max( 0, 1 - m[0,0] + m[1,1] - m[2,2] ) ) / 2; 
		q.z = Mathf.Sqrt( Mathf.Max( 0, 1 - m[0,0] - m[1,1] + m[2,2] ) ) / 2; 
		return q;
	}
	*/

	public static Quaternion GetRotation( Matrix4x4 matrix)
	{
		var qw = Mathf.Sqrt(1f + matrix.m00 + matrix.m11 + matrix.m22) / 2;
		var w = 4 * qw;
		var qx = (matrix.m21 - matrix.m12) / w;
		var qy = (matrix.m02 - matrix.m20) / w;
		var qz = (matrix.m10 - matrix.m01) / w;
		
		return new Quaternion(qx, qy, qz, qw);
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

//		Debug.Log(numCubes);

		Matrix4x4 mtx = new Matrix4x4();
	


		Vector3 xAxis = (axisCubeX.transform.position - axisCubeC.transform.position).normalized;
		Vector3 yAxis = -(axisCubeY.transform.position - axisCubeC.transform.position).normalized;
		Vector3 zAxis = (axisCubeZ.transform.position - axisCubeC.transform.position).normalized;

		mtx.SetColumn(0, xAxis);
		mtx.SetColumn(1, yAxis);
		mtx.SetColumn(2, zAxis);
		//mtx.inverse();

//		jetSki.rotation = GetRotation(mtx);

//		jetSki.up = yAxis;
		jetSki.forward = zAxis;
//		jetSki.Rotate(




		float vInput = Input.GetAxis("Vertical") * propForce;
		motorCubeL.velocity += vInput * zAxis;
		motorCubeR.velocity += vInput * zAxis;

		float hInput = Input.GetAxis("Horizontal") * turnForce;
		motorCubeL.velocity += hInput * xAxis;
		motorCubeR.velocity += hInput * xAxis;

	
	}


}
