using UnityEngine;
using System.Collections;

public class WaterHeightSpring : MonoBehaviour {

	public float waterHeight = 0;
	public float spring = 1;
	public float damp = 1;


	void Start () {
	
	}
	
	void Update () {
	
	}

	void FixedUpdate ()
	{
		// spring physics
		Vector3 posDiff = transform.position - new Vector3 (0,waterHeight,0);
		Vector3 forceToAdd =  - spring * posDiff - damp * rigidbody.GetRelativePointVelocity(new Vector3 (0,waterHeight,0));
		if (transform.position.y < waterHeight)
		{
//			Debug.Log ("below water");
			forceToAdd = new Vector3(0,forceToAdd.y,0);
			rigidbody.AddForce(forceToAdd);
		}


	}
}
