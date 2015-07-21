using UnityEngine;
using System.Collections;

public class Cannonball_Behavior : MonoBehaviour {

	public int AOE;
	public float DMG;

	//  Behavior of cannonball when colliding with ground or enemy
	void OnCollisionEnter(Collision impact)
	{

		// Explodes cannonball upon impact with the ground or an enemy
		if(impact.gameObject.CompareTag("Surface") || impact.gameObject.CompareTag ("Enemy"))
		{
			//Checking if the cannonball AOE hits an enemy.
			Collider[] AOEcheck = Physics.OverlapSphere(gameObject.transform.position, AOE);
			Debug.Log(gameObject.transform.position.ToString ());
			foreach(Collider other in AOEcheck)
			{
				GameObject enemy = other.gameObject;
				if(enemy.gameObject.CompareTag("Enemy"))
				{
					Debug.Log("Hit" + enemy.transform.position.ToString ());
					enemy.SendMessage ("ApplyDamage", DMG, SendMessageOptions.DontRequireReceiver);
				}
			}
				
			Destroy(gameObject);
		}
	}

}
