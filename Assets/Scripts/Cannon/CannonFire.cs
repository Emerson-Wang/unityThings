﻿using UnityEngine;
using System.Collections;

public class CannonFire : MonoBehaviour 
{
	public Rigidbody cannonball;
	private Rigidbody reload;
	private Vector3 shot;
	public Transform shotorigin;
	public int shotspeed;
	private Touch touch;
	public GameObject cannon;
	private float time;
	public float AttackSpeed;
	int layerMask = 1 << 9;

	void Start()
	{
		layerMask = ~layerMask;
		time = 0.0f;
		// anim = GetComponent<Animation>();
	}

	//Checking for input to fire cannonball
	void Update()
	{
		// When left mouse button is pressed, executes "Shoot" function
		if(Input.GetButtonDown ("Fire1"))
		{
			if(time == 0 || Time.time >= time + AttackSpeed)
			{
				time = Time.time;
				MouseShoot();
			}
		}
		else if(Input.touchCount > 0)
		{
			if(time == 0 || Time.time >= time + AttackSpeed)
			{
				time = Time.time;
				TouchShoot();
			}
		}

	}

	void MouseShoot()
	{	
		//Raycast to find out where the cannonball should be fired towards
		RaycastHit shothit;
		//Finding mouse position when left mouse button is pressed
		shot = Input.mousePosition;
		//Translating screen position of mouse to world position on the field.
		Ray mouseClick = Camera.main.ScreenPointToRay(shot);
		//Final vector for firing cannonball
		Vector3 shotdirection;
		//If the click actually lands on the field it will fire the cannonball
		if (Physics.Raycast(mouseClick, out shothit, Mathf.Infinity, layerMask))
		{
			if (shothit.collider.CompareTag ("Surface") || shothit.collider.CompareTag ("Enemy"))
			{
				//Creating cannonball in front of cannon barrel
				reload = Instantiate(cannonball, shotorigin.position, shotorigin.rotation) as Rigidbody;
				//Calculating final shot vector by taking position of mouse and subtracting position of cannon
				shotdirection = shothit.point - reload.transform.position;
				shotdirection.Normalize ();
				//Applying force to cannonball along the vector
				reload.AddForce(shotdirection * shotspeed);
				//Play cannon animation
				GetComponent<Animation>().Play("Cannon_Fire");
			}
		}
	}

	void TouchShoot()
	{	
		touch = Input.GetTouch (0);
		//Raycast to find out where the cannonball should be fired towards
		RaycastHit shothit;
		//Finding mouse position when left mouse button is pressed
		shot = touch.position;
		//Translating screen position of mouse to world position on the field.
		Ray mouseClick = Camera.main.ScreenPointToRay(shot);
		//Final vector for firing cannonball
		Vector3 shotdirection;

		//If the click actually lands on the field it will fire the cannonball
		if (Physics.Raycast(mouseClick, out shothit, Mathf.Infinity, layerMask)) 
		{
			if (shothit.collider.CompareTag ("Surface") || shothit.collider.CompareTag ("Surface"))
			{
				//Creating cannonball in front of cannon barrel
				reload = Instantiate(cannonball, shotorigin.position, shotorigin.rotation) as Rigidbody;
				//Calculating final shot vector by taking position of mouse and subtracting position of cannon
				shotdirection = shothit.point - reload.transform.position;
				shotdirection.Normalize ();
				//Applying force to cannonball along the vector
				reload.AddForce(shotdirection * shotspeed);
				//Play cannon animation
				GetComponent<Animation>().Play("Cannon_Fire");
			}

		}
	}
}