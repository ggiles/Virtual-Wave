using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// this script finds all the other spring joint objects 
// under the same parent and connects all their springs to each other

public class VehicleConnectSprings : MonoBehaviour {

	private SpringJoint[] springJoints; //all the springs on me
	private SpringJoint[] otherSprings; // all the other rigidbodys (with springjoints) that are children of the vehicle
	private List<SpringJoint> otherSpringsList; // otherSprings converted to a list

	void Start () 
	{
		springJoints = gameObject.GetComponents<SpringJoint>(); 
		otherSprings = transform.parent.GetComponentsInChildren<SpringJoint>();
		otherSpringsList = new List<SpringJoint>();
		Debug.Log (otherSprings.Length);
		foreach (SpringJoint otherSpring in otherSprings)
		{
			if (otherSpring.gameObject != gameObject) // make sure I don't add myself to the list, no need to connect to myself
				otherSpringsList.Add(otherSpring);
		}

		for (int i = 1; i < springJoints.Length; i++)
		{
//			Debug.Log ("a spring on me: " + springJoints[i]);
//			Debug.Log ("another vehicle particle: " + otherSpringsList[i]);
			springJoints[i].connectedBody = otherSpringsList[i].rigidbody;
		}
	}
	
	void Update () 
	{

	}
}
