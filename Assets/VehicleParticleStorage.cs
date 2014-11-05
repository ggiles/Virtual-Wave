using UnityEngine;
using System.Collections;

public class VehicleParticleStorage : MonoBehaviour {

	public Vector3 velocity;
	float gravity = -0.6f;
	float maxDampingForce = 0.5f;
	float damping = 0.4f;
	float viscocity = 0.5f;
	float offsetY = 1.5f;


	void Start ()
	{

	}

	void FixedUpdate ()
	{

		transform.position += velocity;

		velocity.y += gravity;

		float h = transform.position.y - offsetY;

		if (h < 0.0f)
		{
			float force = -h * viscocity;
			velocity.y = force;
		}

		/*
		Vector3 dampForce = (velocity * -damping);
		dampForce = Vector3.ClampMagnitude(dampForce, maxDampingForce);

		velocity += dampForce;
	*/



	}

}
