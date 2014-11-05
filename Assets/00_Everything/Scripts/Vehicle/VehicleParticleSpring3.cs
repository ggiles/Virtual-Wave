using UnityEngine;
using System.Collections;

public class VehicleParticleSpring3 : MonoBehaviour {

	public Transform p1;
	public Transform p2;
	private float spring = 0.1f;
	private float damp = 0.1f;
	public float initDistance;

	void Start ()
	{
		initDistance = (p1.position - p2.position).magnitude;

	}	
	
	void FixedUpdate () 
	{
		float newDistance = (p1.position - p2.position).magnitude;

		Vector3 norm = (p1.position - p2.position).normalized;

		Vector3 force = (newDistance - initDistance) * norm;

		p1.position -= (force * spring);

		p2.position += (force * spring);

		Debug.DrawLine(p1.position, p2.position, Color.green);

	}
}
