using UnityEngine;
using System.Collections;

public class BaraccudaTower : MonoBehaviour {

	public GameObject shot;
	private GameObject newShot;
	private bool fired;
	public Vector3 standby;
	private Vector3 spawnLoc;
	private bool firstShot;
	private GameObject lastShot;
	private bool gameEnd;

	// Use this for initialization
	void Start () 
	{
		spawnLoc = transform.position + new Vector3(0, 2, 0);
		firstShot = true;
		standby = transform.position + new Vector3 (0, 4, 0);
		newShot = Instantiate(shot, spawnLoc, Quaternion.identity) as GameObject;
		gameEnd = false;
	}

	void NextShot()
	{
		if(gameEnd == false)
		{
			lastShot = newShot;
			newShot = Instantiate(shot, spawnLoc, Quaternion.identity) as GameObject;
			lastShot.GetComponent<BaraccudaFire>().enabled = true;
		}
	}

	void OnApplicationQuit()
	{
		gameEnd = true;
	}
}
