using UnityEngine;
using System.Collections;

public class BasicShot : MonoBehaviour {

	private GameObject target;
	private Vector3 targetPlace;
	public float projSpeed;

	// Use this for initialization
	void Start () 
	{
		target = gameObject.GetComponent<BaraccudaFire>().target;
		if(gameObject.GetComponent<BaraccudaFire>().dwarfPresent == true)
		{
			projSpeed = 5;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		target = gameObject.GetComponent<BaraccudaFire>().target;
		targetPlace = target.transform.position;
		transform.position = Vector3.MoveTowards (transform.position, targetPlace, projSpeed);
	}
	
}
