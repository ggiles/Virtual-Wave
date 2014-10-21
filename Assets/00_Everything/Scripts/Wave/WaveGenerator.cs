using UnityEngine;
using System.Collections;

[RequireComponent (typeof (WaveGridGenerator))]

public class WaveGenerator : MonoBehaviour {

	WaveGridGenerator wgg;

	public bool bigWave;
	public bool turbulance;

	public float turbulanceIntensity;

	private bool addWaveForce;
	public float waveForce = 100;
	public float waveWaitTime = 5;
	public float waveGoTime = 1;
	

	void Start () {
		wgg = GetComponent<WaveGridGenerator>();
		addWaveForce = false;
		if (bigWave)
			StartCoroutine(BigWaveTimer());
	}
	
	void Update () {

	}

	void FixedUpdate () {
		if (bigWave)
		{
			if (addWaveForce)
			{
				for (int i = 0; i < wgg.gridLength; i++)
				{
					wgg.grid[i,0].rigidbody.AddForce(new Vector3(0,waveForce,0));
					wgg.grid[i,1].rigidbody.AddForce(new Vector3(0,waveForce,0));
				}
			}
		}
		if (turbulance)
		{
			for (int i = 0; i < wgg.gridLength; i++)
			{
				float newY = turbulanceIntensity * Mathf.PerlinNoise(Time.time,0);
				wgg.grid[i,0].rigidbody.AddForce( new Vector3(0,newY,0));
				wgg.grid[i,1].rigidbody.AddForce( new Vector3(0,newY,0));
			}
			for (int i = 0; i < wgg.gridLength; i++)
			{
				float newY = turbulanceIntensity * Mathf.PerlinNoise(Time.time+5,0);
				wgg.grid[i,wgg.gridLength-2].rigidbody.AddForce( new Vector3(0,newY,0));
				wgg.grid[i,wgg.gridLength-1].rigidbody.AddForce( new Vector3(0,newY,0));
			}
			for (int j = 0; j < wgg.gridWidth; j++)
			{
				float newY = turbulanceIntensity * Mathf.PerlinNoise(Time.time+8,0);
				wgg.grid[0,j].rigidbody.AddForce( new Vector3(0,newY,0));
				wgg.grid[1,j].rigidbody.AddForce( new Vector3(0,newY,0));
			}
			for (int j = 0; j < wgg.gridWidth; j++)
			{
				float newY = turbulanceIntensity * Mathf.PerlinNoise(Time.time+15,0);
				wgg.grid[wgg.gridWidth-1,j].rigidbody.AddForce( new Vector3(0,newY,0));
				wgg.grid[wgg.gridWidth-2,j].rigidbody.AddForce( new Vector3(0,newY,0));
			}
		}
	}

	IEnumerator BigWaveTimer ()
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
