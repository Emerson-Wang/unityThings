using UnityEngine;
using System.Collections;

public class Autopathing_Standard : MonoBehaviour {

	private Transform wallPlace;
	private Vector3 newDestination;
	private GameObject wall;
	public float HP;
	public float DMG;

	void ApplyDamage(float DMGTaken)
	{
		HP -= DMGTaken;
	}

	// Use this for initialization
	void Start () 
	{
		wall = GameObject.Find ("enemyDestination");
		wallPlace = wall.GetComponent<Transform>();
		NavMeshAgent enemy = GetComponent<NavMeshAgent>();
		newDestination = new Vector3(wallPlace.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
		enemy.destination = newDestination;
	}

	void Update()
	{
		NavMeshAgent enemy = GetComponent<NavMeshAgent>();
		newDestination = new Vector3(wallPlace.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
		enemy.destination = newDestination;
		if(HP <= 0)
		{
			Destroy(gameObject);
		}
	}

}
