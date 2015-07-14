using UnityEngine;
using System.Collections;

public class Cannonball_Behavior : MonoBehaviour {
	
	//  Behavior of cannonball when colliding with ground or enemy
	void OnCollisionEnter(Collision impact)
	{
		if(impact.gameObject.CompareTag("Surface") || impact.gameObject.CompareTag ("Enemy"))
		{
			Destroy(gameObject);
		}
	}

}
