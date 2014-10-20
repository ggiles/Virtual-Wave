using UnityEngine;
using System.Collections;
using Vectrosity;

public class VehicleLineGenerator : MonoBehaviour {

	public int totalLines = 4;
	VectorLine[] waveLines;
	Vector3[] waveLinesPoints;
	VehicleParticleSpring vps;

	void Start () {

		waveLines = new VectorLine[totalLines];
		waveLinesPoints = new Vector3[totalLines*2];
		vps = gameObject.GetComponent<VehicleParticleSpring>();

		// render all the lines intitially
		for (int i = 0; i < waveLines.Length ; i++)
		{
			if (vps.targetObjects[i] != null)
			{
				waveLinesPoints[i*2] = transform.position;
				Vector3 targetPosition = vps.targetObjects[i].position;
				waveLinesPoints[i*2+1] = targetPosition;
				waveLines[i] = new VectorLine("MyLine", waveLinesPoints, Color.red, null, 2.0f);
			}
		}
	}
	
	void Update () {

		// render all the lines each update
		for (int i = 0; i < waveLines.Length ; i++)
		{
			if (vps.targetObjects[i] != null)
			{
				waveLinesPoints[i*2] = transform.position;
				Vector3 targetPosition = vps.targetObjects[i].position;
				waveLinesPoints[i*2+1] = targetPosition;
				waveLines[i].Draw();
			}
		}

	}
}
