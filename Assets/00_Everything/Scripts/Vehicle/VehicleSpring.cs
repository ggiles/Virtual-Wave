﻿using UnityEngine;
using System.Collections;

public class VehicleSpring : MonoBehaviour {

	public Transform p1;
	private VehicleParticlePhysics p1s;
	public Transform p2;
	private VehicleParticlePhysics p2s;
	private float spring = 0.5f;
	private float damp = 0.8f;
	public float initDistance;

	void Start ()
	{
		initDistance = (p1.position - p2.position).magnitude;
		p1s = p1.gameObject.GetComponent<VehicleParticlePhysics>();
		p2s = p2.gameObject.GetComponent<VehicleParticlePhysics>();

	}	
	
	void FixedUpdate () 
	{
		float newDistance = (p1.position - p2.position).magnitude;

		Vector3 norm = (p1.position - p2.position).normalized;

		Vector3 force = (newDistance - initDistance) * norm;

		float delta = Time.deltaTime;
		delta = 1.0f;

		p1s.velocity -= (force * spring) * delta;
		p1s.velocity *= damp;

		p2s.velocity += (force * spring) * delta;
		p2s.velocity *= damp;

		Debug.DrawLine(p1.position, p2.position, Color.green);

	}
}