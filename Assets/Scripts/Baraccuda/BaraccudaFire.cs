using UnityEngine;
using System.Collections;

public class BaraccudaFire : MonoBehaviour {

	private GameObject[] enemies;
	private GameObject[] dwarves;
	public GameObject target;
	public bool dwarfPresent;
	private float lastDistance;
	public float DMG;
	public float AOE;
	private BasicShot basicShotScript;
	private float distance;
	private GameObject Tower;
	private Collision hit;
	private bool gameEnd;
	private bool primed;
	private int largestMob;
	private Vector3 standby;

	// Use this for initialization
	void Start () 
	{
		Tower = GameObject.Find("BaraccudaTower");
		gameEnd = false;
		standby = gameObject.transform.position + new Vector3 (0, 2, 0);
		primed = false;
	}

	void OnApplicationQuit()
	{
		gameEnd = true;
	}

	void Update()
	{
		if(gameObject.transform.position == standby)
		{
			primed = true;
		}
		if(target == null && primed == true)
		{
			targetSelection();
		}
		if(primed != true)
		{
			gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, standby, 0.05f);
		}
	}

	// If there are no dwarves, will only perform basic shots, which are slow, medium damage homing shots
	void BasicShot()
	{
		lastDistance = 500f;

		// Checking for closest enemy
		foreach(GameObject enemy in enemies)
		{
			Vector3 destination = enemy.GetComponent<Autopathing_Standard>().newDestination;
			distance = Vector3.Distance(enemy.transform.position, destination);
			if (distance < lastDistance)
			{
				target = enemy;
				lastDistance = distance;
			}
		}

	}

	// Shot for dwarves
	void dwarfShot()
	{
		largestMob = 0;
		foreach(GameObject dwarf in dwarves)
		{
			int mobSize = dwarf.GetComponent<mobSize>().size;
			if (mobSize >= largestMob)
			{
				target = dwarf;
				largestMob = mobSize;
			}
			Debug.Log (target.ToString());
		}
	}
	
	void OnCollisionEnter(Collision impact)
	{
		hit = impact;
		Destroy(gameObject);
	}


	void targetSelection()
	{
		// Searching for enemies and specifically dwarves
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		// Destroy projectile if there are no enemies
		if(enemies.Length == 0)
		{
			Destroy(gameObject);
		}
		dwarves = GameObject.FindGameObjectsWithTag("Dwarf");
		//If there are dwarves, use different attack
		if(dwarves.Length > 0)
		{
			dwarfPresent = true;
			dwarfShot();
			basicShotScript = gameObject.GetComponent<BasicShot>();
			basicShotScript.enabled = true;
		}
		//Execute basic attack
		else
		{
			dwarfPresent = false;
			BasicShot();
			basicShotScript = gameObject.GetComponent<BasicShot>();
			basicShotScript.enabled = true;
			
		}
	}

	void OnDestroy()
	{
		if(dwarfPresent == true && gameEnd == false)
		{
			AOE = 10;
			DMG = 50;
			// Explodes projectile
			if(hit.gameObject.CompareTag("Surface") || hit.gameObject.CompareTag ("Enemy"))
			{
				
				//Checking if the AOE hits an enemy.
				Collider[] AOEcheck = Physics.OverlapSphere(gameObject.transform.position, AOE);
				//GameObject boom = Instantiate(explosion, ballInfo, Quaternion.identity) as GameObject;
				//Destroy(boom.gameObject, 1.5F);
				foreach(Collider other in AOEcheck)
				{
					GameObject enemy = other.gameObject;
					if(enemy.CompareTag("Enemy"))
					{
						enemy.SendMessage ("ApplyDamage", DMG, SendMessageOptions.DontRequireReceiver);
					}
				}
				
			}
			Tower.SendMessage("NextShot", SendMessageOptions.DontRequireReceiver);
		}
		else if (gameEnd == false && target != null)
		{
			hit.gameObject.SendMessage ("ApplyDamage", DMG);
			Tower.SendMessage("NextShot", SendMessageOptions.DontRequireReceiver);
		}
	}

	void NextShot()
	{
		Tower.SendMessage("NextShot", SendMessageOptions.DontRequireReceiver);
	}
}
