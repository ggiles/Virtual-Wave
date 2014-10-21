using UnityEngine;
using System.Collections;
using Vectrosity;

[RequireComponent (typeof (WaveParticleSpring))]

public class WaveLineGenerator : MonoBehaviour {

	public bool lines3D;
	public int totalLines = 4;
	VectorLine[] waveLines;
	Vector3[] waveLinesPoints;
	WaveParticleSpring wps;

	void Start () {

		waveLines = new VectorLine[totalLines];
		waveLinesPoints = new Vector3[totalLines*2];
		wps = gameObject.GetComponent<WaveParticleSpring>();

		// render all the lines intitially
		for (int i = 0; i < waveLines.Length ; i++)
		{
			if (wps.targetObjects[i] != null)
			{
				waveLinesPoints[i*2] = transform.position;
				Vector3 targetPosition = wps.targetObjects[i].position;
				waveLinesPoints[i*2+1] = targetPosition;
				waveLines[i] = new VectorLine("MyLine", waveLinesPoints, Color.red, null, 2.0f);
			}
		}
	}
	
	void Update () {

		// render all the lines each update
		for (int i = 0; i < waveLines.Length ; i++)
		{
			if (wps.targetObjects[i] != null)
			{
				waveLinesPoints[i*2] = transform.position;
				Vector3 targetPosition = wps.targetObjects[i].position;
				waveLinesPoints[i*2+1] = targetPosition;
				if (!lines3D) {
					waveLines[i].Draw();
				}else{
					waveLines[i].Draw3D();
				}
			}
		}

	}
}
