using UnityEngine;
using System.Collections;
using Vectrosity; 

public class VehicleGridGenerator : MonoBehaviour {

	public GameObject vehicleParticle;
	public int gridLength = 2;
	public int gridWidth = 2;
	public int gridHeight = 2;
	public float gridDistance = 2;
	private int connections;
	GameObject[,,] grid;

	void Start () 
	{

		grid = new GameObject[gridLength,gridWidth,gridHeight]; 
		connections = gridLength + gridWidth + gridHeight - 1;
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
					grid[i,j,k] = Instantiate(vehicleParticle, new Vector3(x,y,z-gridDistance),Quaternion.identity) as GameObject;
					grid[i,j,k].transform.parent = transform;
					// add springs
					for (int sj = 0; sj < connections; sj++)
					{
						grid[i,j,k].AddComponent<SpringJoint>();
					}
					y += gridDistance;
				}
				x += gridDistance;
			}
		
			z += gridDistance * 3f;
		}

	}
	
	void Update ()
	{
		
	}
}
