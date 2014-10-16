using UnityEngine;
using System.Collections;

public class WaveGridGenerator : MonoBehaviour {

	public GameObject waveParticle;
	public int gridSize = 10;
	public float gridDistance = 2;
	GameObject[,] grid;

	void Start () 
	{
		grid = new GameObject[gridSize,gridSize]; 

		// generate the grid
		float z = 0;
		for (int i = 0; i < gridSize; i++)
		{
			float x = 0;
			for (int j = 0; j < gridSize; j++)
			{
				grid[i,j] = Instantiate(waveParticle, new Vector3(x,0,z),Quaternion.identity) as GameObject;
				grid[i,j].transform.parent = transform;
				x += gridDistance;
			}
		
			z += gridDistance;
		}

		// link the springs
		for (int i = 0; i < gridSize; i++)
		{
			for (int j = 0; j < gridSize; j++)
			{
				if (i+1 < gridSize)
					grid[i,j].GetComponent<WaveParticleSpring>().targetObjects[0] = grid[i+1,j].transform;
				if (i-1 > 0)
					grid[i,j].GetComponent<WaveParticleSpring>().targetObjects[1] = grid[i-1,j].transform;
				if (j+1 < gridSize)
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
