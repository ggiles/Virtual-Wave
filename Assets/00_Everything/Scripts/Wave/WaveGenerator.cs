using UnityEngine;
using System.Collections;

[RequireComponent (typeof (WaveGridGenerator))]

public class WaveGenerator : MonoBehaviour {

	WaveGridGenerator wgg;

	private bool addWaveForce;
	public float waveForce = 100;
	public float waveWaitTime = 5;
	public float waveGoTime = 1;

	void Start () {
		wgg = GetComponent<WaveGridGenerator>();
		addWaveForce = false;
		StartCoroutine(WaveTimer());
	}
	
	void Update () {

	}

	void FixedUpdate () {
		if (addWaveForce)
		{
			for (int i = 0; i < wgg.gridLength; i++)
			{
				wgg.grid[i,0].rigidbody.AddForce(new Vector3(0,waveForce,0));
				wgg.grid[i,1].rigidbody.AddForce(new Vector3(0,waveForce,0));
			}
		}
	}

	IEnumerator WaveTimer ()
	{
		while (true)
		{
			addWaveForce = true;
			yield return new WaitForSeconds(waveGoTime);
			addWaveForce = false;
			yield return new WaitForSeconds(waveWaitTime);

		}
	}
}
