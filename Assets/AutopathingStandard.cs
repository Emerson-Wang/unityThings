using UnityEngine;
using System.Collections;

public class AutopathingStandard : MonoBehaviour {

	public Transform wall;

	// Use this for initialization
	void Start () 
	{
		NavMeshAgent enemy = GetComponent<NavMeshAgent>();
		enemy.destination = wall.position;
	}
	
}
