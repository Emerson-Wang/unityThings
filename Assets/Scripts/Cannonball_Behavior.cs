using UnityEngine;
using System.Collections;

public class Cannonball_Behavior : MonoBehaviour {

	public int AOE;
	public float DMG;
	public GameObject explosion;
	private int hitCount = 0;


	//  Behavior of cannonball when colliding with ground or enemy
	void OnCollisionEnter(Collision impact)
	{

		hitCount += 1;

		Debug.Log ("hitting" + impact.gameObject.ToString ());

		Collider ball = gameObject.GetComponent<Collider>();

		ball.enabled = false;

		Debug.Log (Time.time.ToString ());

		Vector3 ballInfo = gameObject.transform.position;
		
		Destroy(gameObject);	

		// Explodes cannonball upon impact with the ground or an enemy
		if(impact.gameObject.CompareTag("Surface") || impact.gameObject.CompareTag ("Enemy"))
		{

			//Checking if the cannonball AsOE hits an enemy.
			Collider[] AOEcheck = Physics.OverlapSphere(ballInfo, AOE);
			GameObject boom = Instantiate(explosion, ballInfo, Quaternion.identity) as GameObject;
			Destroy(boom.gameObject, 1.5F);
			foreach(Collider other in AOEcheck)
			{
				Debug.Log(other.ToString ());
				GameObject enemy = other.gameObject;
				if(enemy.CompareTag("Enemy") || hitCount == 1)
				{
					enemy.SendMessage ("ApplyDamage", DMG, SendMessageOptions.DontRequireReceiver);
				}
			}

		}
	}

	void OnCollisionExit(Collision impact)
	{
		Destroy(gameObject);
	}
}
