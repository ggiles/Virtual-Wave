using UnityEngine;
using System.Collections;
using Vectrosity; 

public class VehicleGridGenerator : MonoBehaviour {

	public GameObject vehicleParticle;
	public GameObject vehicleParticleTarget;
	public int gridLength = 4;
	public int gridWidth = 2;
	public int gridHeight = 1;
	public float gridDistance = 2;
	public int connections = 7;
	GameObject[,,] grid;
	public bool makeTargetGrid;
	public GameObject[,,] targetGrid;

	void Start () 
	{

		grid = new GameObject[gridLength,gridWidth,gridHeight]; 
		if (makeTargetGrid)
			targetGrid = new GameObject[gridLength,gridWidth,gridHeight]; 


		// generate the grid
		float z = transform.position.z - (((gridLength-1)*gridDistance)/2);
		for (int i = 0; i < gridLength; i++)
		{
			float x = transform.position.x - (((gridWidth-1)*gridDistance)/2);
			for (int j = 0; j < gridWidth; j++)
			{
				float y = transform.position.y - (gridDistance/2);
				for (int k = 0; k < gridHeight; k++)
				{
					grid[i,j,k] = Instantiate(vehicleParticle, new Vector3(x,y,z),Quaternion.identity) as GameObject;
					if (makeTargetGrid)
						targetGrid[i,j,k] = Instantiate(vehicleParticleTarget, new Vector3(x,y,z),Quaternion.identity) as GameObject;
					grid[i,j,k].transform.parent = transform;
					if (makeTargetGrid)
						targetGrid[i,j,k].transform.parent = transform;
					y += gridDistance;
				}
				x += gridDistance;
			}
		
			z += gridDistance;
		}

		// link the springs and set the target objects
		for (int i = 0; i < gridLength; i++)
		{
			for (int j = 0; j < gridWidth; j++)
			{
				for (int k = 0; k < gridHeight; k++)
				{
					// set the fixed joint rigidbody to the parent
					grid[i,j,k].GetComponent<FixedJoint>().connectedBody = rigidbody;

					VehicleParticleSpring gridPoint = grid[i,j,k].GetComponent<VehicleParticleSpring>();// get the spring component
					if (makeTargetGrid)
					{
						VehicleParticlePositionSpring positionSpring = grid[i,j,k].GetComponent<VehicleParticlePositionSpring>();
						positionSpring.targetObject = targetGrid[i,j,k];
					}

					int index = 0;
					for (int ii = 0; ii < gridLength; ii++)
					{
						for (int jj = 0; jj < gridWidth; jj++)
						{
							for (int kk = 0; kk < gridHeight; kk++)
							{
								if (j != jj || i != ii || k != kk)
								{
									gridPoint.targetObjects[index] = grid[ii,jj,kk].transform;
									index += 1;
	//								Debug.Log ("grid["+ii+","+jj+"]");
								}
							}
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
