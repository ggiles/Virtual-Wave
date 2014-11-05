using UnityEngine;
using System.Collections;

public class VehicleParticlePhysics : MonoBehaviour {
	[HideInInspector]
	public Vector3 velocity;
	float gravity = -0.5f;
	float damping = 0.98f;
	float viscocity = 0.3f;
	float offsetY = 1.5f;
	float waterHeight;


	void Start ()
	{

	}

	void FixedUpdate ()
	{

		transform.position += velocity;

		velocity.y += gravity * Time.deltaTime;

		float h = transform.position.y - offsetY;

		if (h < waterHeight)
		{
			float force = -h * viscocity;
			velocity.y += force * Time.deltaTime;
			velocity *= damping;
		}


	}

}
