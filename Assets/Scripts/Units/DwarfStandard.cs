using UnityEngine;
using System.Collections;

public class Autopathing_Standard : MonoBehaviour {

	private Transform wallPlace;
	public Vector3 newDestination;
	private GameObject wall;
	public float HP;
	public float DMG;
	public GameObject HPleft;
	private float OriginHP;
	public GameObject HPCap;
	public float AttackRange;
	public float AttackSpeed;
	private bool InRange;
	private bool LastLoc;
	private float time;
	private bool WallAlive;
	private bool AtDoor;
	private GameObject TownWall;
	private NavMeshAgent enemy;
	private WallScript wallCode;


	// Checking if enemy is at the door
	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Finish"))
		{
			TownWall.SendMessage ("LoseFood");
			Destroy(gameObject);
		}
	}
	// Method for taking damage from enemies and showing it on HP bar	
	void ApplyDamage(float DMGTaken)
	{
		HP -= DMGTaken;
		if (HP != OriginHP)
		{
			HPCap.gameObject.SetActive (false);
		}

		// Destroy unit if HP drops below 0
		if(HP <= 0)
		{
			Destroy(gameObject);
		}

		// Updating HP bar
		float HPBarMinus = (HP * 1f)/OriginHP;
		HPleft.transform.localScale = new Vector3(1, HPBarMinus, 1);

	
	}

	// Use this for initialization
	void Start () 
	{
		TownWall = GameObject.Find ("Helms Deep");
		wallCode = TownWall.GetComponent <WallScript>();
		// Setting the navigation to autopath the enemies to the wall
		OriginHP = HP;
		// Getting position of wall
		wall = GameObject.Find ("enemyDestination");
		wallPlace = wall.GetComponent<Transform>();
		// Getting NavMesh component
		enemy = GetComponent<NavMeshAgent>();
		enemy.enabled = true;
		// Setting destination of enemy
		newDestination = new Vector3(wallPlace.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
		enemy.destination = newDestination;
	}

	void Update()
	{
		// Causing the enemies to start dealing damage upon getting close enough to the wall.
		wallPlace = wall.GetComponent<Transform>();
		float wallDistance = (newDestination - gameObject.transform.position).magnitude;

		//Checking if range status has changed
		LastLoc = InRange;

		if (wallCode.WallDown == false)
		{
			// Checking if in attack range
			if(wallDistance <= AttackRange)
			{
				InRange = true;

				enemy.enabled = false;


			}
			// Not in attack range
			else if(wallDistance > AttackRange)
			{
				InRange = false;
				enemy.enabled = true;
				// Updating destination of enemies so that they're always walking straight towards the wall
				newDestination = new Vector3(wallPlace.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
				enemy.destination = newDestination;
			}

			// Upon entering attack range, sets time of entering attack range and attacks
			if (InRange == true && LastLoc != InRange)
			{
				TownWall.SendMessage ("ApplyDamage", DMG, SendMessageOptions.DontRequireReceiver);
				time = Time.time;
			}
			// Continues attack at intervals set by the float AttackSpeed
			if(InRange == true && Time.time >= time + AttackSpeed)
			{
				TownWall.SendMessage ("ApplyDamage", DMG, SendMessageOptions.DontRequireReceiver);
				time = Time.time;
			}
		}

		else
		{
			// Changing destination to the center of the wall
			enemy.enabled = true;
			enemy.destination = wallPlace.position;

		}
			
		
	}
	


}
