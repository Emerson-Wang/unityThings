using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {

	public float HP;
	public int food;
	private float OriginHP;
	public bool WallDown;
	public GameObject door;

	// Use this for initialization
	void Start () 
	{
		door = GameObject.Find ("door");
		door.gameObject.SetActive (false);
		OriginHP = HP;
		WallDown = false;
	}
	
	void ApplyDamage(float DMGTaken)
	{
		if(HP > 0)
		{
		HP -= DMGTaken;
		}
		
		// Allow enemies into wall if HP drops below 0
		else if(HP <= 0)
		{
			WallDown = true;
			door.gameObject.SetActive(true);
			/* while(food != 0)
			{
				GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
				foreach(GameObject enemy in enemies)
				{
					enemy.SendMessage("WallDown");
				}
			}
			*/
		
		}
		
		/* Update health bar
		float HPBarMinus = (HP * 1f)/OriginHP;
		HPleft.transform.localScale = new Vector3(1, HPBarMinus, 1);
		*/
	}

	void LoseFood()
	{
		food -= 1;
		if(food <= 0)
		{
			Debug.Log ("Game Over");
			Application.Quit ();
		}

	}
}
