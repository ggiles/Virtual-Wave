using UnityEngine;
using System.Collections;

[RequireComponent (typeof (WaveGridGenerator))]

public class WaveGridMover : MonoBehaviour {

	WaveGridGenerator wgg;
	public Transform player;
	public Vector3 lastPlayerPos;


	void Start () {
		wgg = GetComponent<WaveGridGenerator>();
		lastPlayerPos = player.position;
	}

	void FixedUpdate () {

		float gd = wgg.gridDistance;
		// if the player has moved more than one grid space
		if (lastPlayerPos.x - player.position.x >= gd)
		{
			Debug.Log ("hit grid distance");
			lastPlayerPos = new Vector3(Mathf.Round(player.position.x),0,0);
			// move the grid one grid space forward
			transform.position -= new Vector3(gd, 0,0);
			// move the grid data one grid space back
			MoveHeightData("j");
		}

	}

	void MoveHeightData (string dimension)
	{		
		for (int i = 0; i < wgg.gridLength; i++)
		{
			for (int j = 0; j < wgg.gridWidth; j++)
			{
				if (j-1 > 0)
				{
					float newY = 0;
					if (dimension == "i")
						newY = wgg.grid[i+1,j].transform.position.y;
					else if (dimension == "j")
						newY = wgg.grid[i,j-1].transform.position.y;
					Vector3 newPos = new Vector3(wgg.grid[i,j].transform.position.x, newY, wgg.grid[i,j].transform.position.z);
					wgg.grid[i,j].rigidbody.MovePosition(newPos);
				}
			}
		}

	}

}


