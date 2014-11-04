using UnityEngine;
using System.Collections;

[RequireComponent (typeof (WaveGridGenerator))]

// this script moves the grid along with you, 
// while at the same time moving the data within the grid in the opposite way

public class WaveGridMover : MonoBehaviour {

	WaveGridGenerator wgg;
	public Transform player;
	public float lastPlayerPosX;
	public float lastPlayerPosZ;

	void Start () 
	{
		wgg = GetComponent<WaveGridGenerator>();
		lastPlayerPosX = player.position.x;
		lastPlayerPosZ = player.position.z;
	}

	void FixedUpdate () 
	{

		float gd = wgg.gridDistance;

//		Debug.Log (lastPlayerPos.x - player.position.x);

		// if the player has moved more than one grid space
		float xDif = lastPlayerPosX - player.position.x;
		float zDif = lastPlayerPosZ - player.position.z;
//		Debug.Log ("xDif: " + xDif);
//		Debug.Log ("zDif: " + zDif);

		if ( xDif >= gd )
		{

			if (xDif <= gd)
				MoveGrid(player.position.x, new Vector3(-gd,0,0), "j-");
			else
			{
				float distanceToMove = -gd * Mathf.Floor(xDif/gd);
				MoveGrid(player.position.x, new Vector3(distanceToMove,0,0), "j-");
			}
		}

		if ( xDif <= -gd )
		{

			if (xDif <= gd)
				MoveGrid(player.position.x, new Vector3(gd,0,0), "j+");
			else
			{
				float distanceToMove = gd * Mathf.Floor(xDif/gd);
				MoveGrid(player.position.x, new Vector3(distanceToMove,0,0), "j+");
			}
		}

		if ( zDif >= gd )
		{

			if (zDif <= gd)
				MoveGrid(player.position.z, new Vector3(-gd,0,0), "i-");
			else
			{
				float distanceToMove = -gd * Mathf.Floor(zDif/gd);
				MoveGrid(player.position.z, new Vector3(0,0,distanceToMove), "i-");
			}
		}

		if ( zDif <= -gd )
		{

			if (zDif <= gd)
			{
				MoveGrid(player.position.z, new Vector3(0,0,gd), "i+");
			}
			else
			{
				float distanceToMove = gd * Mathf.Floor(zDif/gd);
				MoveGrid(player.position.z, new Vector3(0,0,distanceToMove), "i+");
			}
		}

	}

	// if the player moved one grid space this gets called
	void MoveGrid (float playerPos, Vector3 gridDist, string dimension)
	{
		// set the last player pos
		if (dimension == "j-" || dimension == "j+")
			lastPlayerPosX = Mathf.Round(playerPos);
		if (dimension == "i-" || dimension == "i+")
			lastPlayerPosZ = Mathf.Round(playerPos);

		// move the grid one grid space forward
		transform.position += gridDist;
		// move the grid data one grid space back
		MoveHeightData(dimension);
	}

	// moves height data along the grid by 1 grid space
	void MoveHeightData (string dimension)
	{		
		for (int i = 0; i < wgg.gridLength; i++)
		{
			for (int j = 0; j < wgg.gridWidth; j++)
			{
	
				if (dimension == "j-")
				{
					if (j-1 > 0)
					{
						float newY = wgg.grid[i,j-1].transform.position.y;
						SetHeightData(newY,i,j);
					}
				}
				if (dimension == "j+")
				{
					if (j+1 < wgg.gridLength)
					{
						float newY = wgg.grid[i,j+1].transform.position.y;
						SetHeightData(newY,i,j);
					}
				}
				if (dimension == "i-")
				{
					if (i-1 > 0)
					{
						float newY = wgg.grid[i-1,j].transform.position.y;
						SetHeightData(newY,i,j);
					}
				}
				if (dimension == "i+")
				{
					if (i+1 < wgg.gridWidth)
					{
						float newY = wgg.grid[i+1,j].transform.position.y;
						SetHeightData(newY,i,j);
					}
				}
			
			}
		}
	}

	void SetHeightData (float newY, int i, int j)
	{
		Vector3 newPos = new Vector3(wgg.grid[i,j].transform.position.x, newY, wgg.grid[i,j].transform.position.z);
		wgg.grid[i,j].rigidbody.MovePosition(newPos);
	}

}


