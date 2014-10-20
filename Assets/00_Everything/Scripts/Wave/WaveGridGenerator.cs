using UnityEngine;
using System.Collections;
using Vectrosity; 

public class WaveGridGenerator : MonoBehaviour {

	public GameObject vehicleParticle;
	public int gridLength = 8;
	public int gridWidth = 2;
	public float gridDistance = 2;
	GameObject[,] grid;

	void Start () 
	{

		grid = new GameObject[gridLength,gridWidth]; 

		// generate the grid
		float z = 0;
		for (int i = 0; i < gridLength; i++)
		{
			float x = 0;
			for (int j = 0; j < gridWidth; j++)
			{
				grid[i,j] = Instantiate(vehicleParticle, new Vector3(x,0,z),Quaternion.identity) as GameObject;
				grid[i,j].transform.parent = transform;
				x += gridDistance;
			}
		
			z += gridDistance;
		}

		// link the springs
		for (int i = 0; i < gridLength; i++)
		{
			for (int j = 0; j < gridWidth; j++)
			{
				if (i+1 < gridLength)
					grid[i,j].GetComponent<WaveParticleSpring>().targetObjects[0] = grid[i+1,j].transform;
				if (i-1 > 0)
					grid[i,j].GetComponent<WaveParticleSpring>().targetObjects[1] = grid[i-1,j].transform;
				if (j+1 < gridWidth)
					grid[i,j].GetComponent<WaveParticleSpring>().targetObjects[2] = grid[i,j+1].transform;
				if (j-1 > 0)
					grid[i,j].GetComponent<WaveParticleSpring>().targetObjects[3] = grid[i,j-1].transform;
			}
		}
	}
	
	void Update ()
	{
		
	}
}
