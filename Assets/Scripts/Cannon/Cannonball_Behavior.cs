using UnityEngine;
using System.Collections;

public class Cannonball_Behavior : MonoBehaviour {

	public int AOE;
	public float DMG;
	public GameObject explosion;
	private Vector3 ballInfo;
	private Collision ballImpact;


	//  Behavior of cannonball when colliding with ground or enemy
	void OnCollisionEnter(Collision impact)
	{

		ballInfo = gameObject.transform.position;
		
		Destroy(gameObject);	

		ballImpact = impact;

	}

	void OnDestroy()
	{
		// Explodes cannonball upon impact with the ground or an enemy
		if(ballImpact.gameObject.CompareTag("Surface") || ballImpact.gameObject.CompareTag ("Enemy"))
		{
			
			//Checking if the cannonball AsOE hits an enemy.
			Collider[] AOEcheck = Physics.OverlapSphere(ballInfo, AOE);
			GameObject boom = Instantiate(explosion, ballInfo, Quaternion.identity) as GameObject;
			Destroy(boom.gameObject, 1.5F);
			foreach(Collider other in AOEcheck)
			{
				GameObject enemy = other.gameObject;
				if(enemy.CompareTag("Enemy"))
				{
					enemy.SendMessage ("ApplyDamage", DMG, SendMessageOptions.DontRequireReceiver);
				}
			}
			
		}
	}
}
