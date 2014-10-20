using UnityEngine;
using System.Collections;
using Vectrosity; 

public class VehicleGridGenerator : MonoBehaviour {

	public GameObject vehicleParticle;
	public GameObject vehicleParticleTarget;
	public int gridLength = 4;
	public int gridWidth = 2;
	public float gridDistance = 2;
	public int connections = 7;
	GameObject[,] grid;
	public GameObject[,] targetGrid;

	void Start () 
	{

		grid = new GameObject[gridLength,gridWidth]; 
		targetGrid = new GameObject[gridLength,gridWidth]; 


		// generate the grid
		float z = transform.position.z;
		for (int i = 0; i < gridLength; i++)
		{
			float x = transform.position.x;
			for (int j = 0; j < gridWidth; j++)
			{
				grid[i,j] = Instantiate(vehicleParticle, new Vector3(x,transform.position.y,z),Quaternion.identity) as GameObject;
				targetGrid[i,j] = Instantiate(vehicleParticleTarget, new Vector3(x,transform.position.y,z),Quaternion.identity) as GameObject;
				grid[i,j].transform.parent = transform;
				targetGrid[i,j].transform.parent = transform;

				x += gridDistance;
			}
		
			z += gridDistance;
		}

		// link the springs and set the target objects
		for (int i = 0; i < gridLength; i++)
		{
			for (int j = 0; j < gridWidth; j++)
			{
				VehicleParticleSpring gridPoint = grid[i,j].GetComponent<VehicleParticleSpring>();
				VehicleParticlePositionSpring positionSpring = grid[i,j].GetComponent<VehicleParticlePositionSpring>();
				positionSpring.targetObject = targetGrid[i,j];

				int index = 0;
				for (int ii = 0; ii < gridLength; ii++)
				{
						for (int jj = 0; jj < gridWidth; jj++)
						{
							if (j != jj || i != ii)
							{
								gridPoint.targetObjects[index] = grid[ii,jj].transform;
								index += 1;
//								Debug.Log ("grid["+ii+","+jj+"]");
							}
						}
				}
			}
		}
	}
	
	void Update ()
	{
		
	}
}
