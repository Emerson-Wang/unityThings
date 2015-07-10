using UnityEngine;
using System.Collections;

public class Autopathing_Standard : MonoBehaviour {

	public Transform wall;
	private Vector3 newDestination;

	// Use this for initialization
	void Start () 
	{
		NavMeshAgent enemy = GetComponent<NavMeshAgent>();
		newDestination = new Vector3(wall.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
		enemy.destination = newDestination;
	}

}
