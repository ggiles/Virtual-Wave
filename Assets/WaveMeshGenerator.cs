using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveMeshGenerator : MonoBehaviour {

	public int width = 10;
	public float spacing = 1f;
	public float maxHeight = 3f;
	public MeshFilter terrainMesh = null;
	
	void Start()
	{
		if (terrainMesh == null)
		{
			Debug.LogError("ProceduralTerrain requires its target terrainMesh to be assigned.");
		}
		
		GenerateMesh();
	}

	void GenerateMesh ()
	{
		float start_time = Time.time;
		
		List<Vector3[]> verts = new List<Vector3[]>();
		List<int> tris = new List<int>();
		List<Vector2> uvs = new List<Vector2>();
		
		// Generate everything.
		for (int z = 0; z < width; z++)
		{
			verts.Add(new Vector3[width]);
			for (int x = 0; x < width; x++)
			{
				Vector3 current_point = new Vector3();
				current_point.x = x * spacing;
				current_point.z = z * spacing; // TODO this makes right triangles, fix it to be equilateral
				
				current_point.y = GetHeight(current_point.x, current_point.z);
				
				verts[z][x] = current_point;
				uvs.Add(new Vector2(x,z)); // TODO Add a variable to scale UVs.
			}
		}
		
		// Only generate one triangle.
		// TODO Generate a grid of triangles.
		tris.Add(0);
		tris.Add(1);
		tris.Add(width);
		
		// Unfold the 2d array of verticies into a 1d array.
		Vector3[] unfolded_verts = new Vector3[width*width];
		int i = 0;
		foreach (Vector3[] v in verts)
		{
			v.CopyTo(unfolded_verts, i * width);
			i++;
		}
		
		// Generate the mesh object.
		Mesh ret = new Mesh();
		ret.vertices = unfolded_verts;
		ret.triangles = tris.ToArray();
		ret.uv = uvs.ToArray();
		
		// Assign the mesh object and update it.
		ret.RecalculateBounds();
		ret.RecalculateNormals();
		terrainMesh.mesh = ret;
		
		float diff = Time.time - start_time;
		Debug.Log("ProceduralTerrain was generated in " + diff + " seconds.");
	}
	
	// Return the terrain height at the given coordinates.
	// TODO Currently it only makes a single peak of max_height at the center,
	// we should replace it with something fancy like multi-layered perlin noise sampling.
	float GetHeight(float x_coor, float z_coor)
	{
		float y_coor = Mathf.Min( 0, maxHeight - Vector2.Distance(Vector2.zero, new Vector2(x_coor, z_coor)));
		return y_coor;
	}

}
