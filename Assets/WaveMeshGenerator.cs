// http://kobolds-keep.net/
// This code is released under the Creative Commons 0 License. https://creativecommons.org/publicdomain/zero/1.0/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(WaveGridGenerator))]

public class WaveMeshGenerator : MonoBehaviour {
	
	private int width;
	private float spacing;
	public MeshFilter terrainMesh = null;	
	private WaveGridGenerator wgg; 

	List<Vector3[]> verts = new List<Vector3[]>();
	List<int> tris = new List<int>();
	List<Vector2> uvs = new List<Vector2>();
	Vector3[] unfolded_verts;

	Mesh ret;
	
	void Start()
	{
		wgg = gameObject.GetComponent<WaveGridGenerator>();
		width = wgg.gridWidth;
		spacing = wgg.gridDistance;
		unfolded_verts = new Vector3[width*width];
		ret = new Mesh ();

		GenerateMesh();
	}
	
	void GenerateMesh()
	{
		// Generate everything.
		for (int z = 0; z < width; z++)
		{
			verts.Add(new Vector3[width]);
			for (int x = 0; x < width; x++)
			{
				Vector3 current_point = new Vector3();
				current_point.x = x * spacing;
				current_point.z = z * spacing;

				// Triangular grid offset
//				int offset = z % 2;
//				if (offset == 1)
//				{
//					current_point.x -= spacing * 0.5f;
//				}

				current_point.y = GetHeight(x, z);

				verts[z][x] = current_point;
				uvs.Add(new Vector2(x,z)); // TODO Add a variable to scale UVs.

				// Don't generate a triangle if it would be out of bounds.
				int current_x = x;
				if (current_x <= 0 || z <= 0 || current_x >= width)
				{
					continue;
				}
				// Generate the triangle north of you.
				tris.Add(x + z*width);
				tris.Add(current_x + (z-1)*width);
				tris.Add((current_x-1) + (z-1)*width);
				
				// Generate the triangle northwest of you.
				if (x <= 0 || z <= 0)
				{
					continue;
				}
				tris.Add(x + z*width);
				tris.Add((current_x-1) + (z-1)*width);
				tris.Add((x-1) + z*width);
			}
		}
		
		UnfoldVerts();
		RefreshMeshData();
		terrainMesh.mesh = ret;

	}

	void Update ()
	{
		for (int z = 0; z < width; z++)
		{
			for (int x = 0; x < width; x++)
			{
				verts[x][z].y = GetHeight(x,z);
//				verts[x][z].y = 0;
				UnfoldVerts();
				RefreshMeshData();

			}
		}
	}

	void UnfoldVerts ()
	{
		// Unfold the 2d array of verticies into a 1d array.
		int iv = 0;
		foreach (Vector3[] v in verts)
		{
			v.CopyTo(unfolded_verts, iv * width);
			iv++;
		}
	}

	void RefreshMeshData ()
	{
		ret.Clear();
		// Generate the mesh object.
		ret.vertices = unfolded_verts;
		ret.triangles = tris.ToArray ();
		ret.uv = uvs.ToArray ();

		// Assign the mesh object and update it.
//		ret.RecalculateBounds();
		ret.RecalculateNormals();
	}

	// Return the terrain height at the given coordinates.
	float GetHeight(int x, int z)
	{
//		Debug.Log ("x: " + x + " z: " + z);
//		Debug.Log (wgg.grid[0,0]);
		float y = wgg.grid[x,z].transform.position.y;
//		float y = 0;
		return y;
	}
}