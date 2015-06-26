using UnityEngine;
using System.Collections;

public class CannonFire : MonoBehaviour 
{
	public Rigidbody cannonball;
	private Rigidbody reload;
	private Vector3 shot;
	public Transform shotorigin;
	public int shotspeed;
	private Touch touch;
	
	//Checking for input to fire cannonball
	void Update()
	{
		// When left mouse button is pressed, executes "Shoot" function
		if(Input.GetButtonDown ("Fire1"))
		{
			MouseShoot();
		}
		else if(Input.touchCount > 0)
		{
			TouchShoot();
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

		//Creating cannonball in front of cannon barrel
		reload = Instantiate(cannonball, shotorigin.position, shotorigin.rotation) as Rigidbody;

		//If the click actually lands on the field it will fire the cannonball
		if (Physics.Raycast(mouseClick, out shothit))
		{
			//Calculating final shot vector by taking position of mouse and subtracting position of cannon
			shotdirection = shothit.point - reload.transform.position;
			//Applying force to cannonball along the vector
			reload.AddForce(shotdirection * shotspeed);
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
		
		//Creating cannonball in front of cannon barrel
		reload = Instantiate(cannonball, shotorigin.position, shotorigin.rotation) as Rigidbody;
		
		//If the click actually lands on the field it will fire the cannonball
		if (Physics.Raycast(mouseClick, out shothit))
		{
			//Calculating final shot vector by taking position of mouse and subtracting position of cannon
			shotdirection = shothit.point - reload.transform.position;
			//Applying force to cannonball along the vector
			reload.AddForce(shotdirection * shotspeed);
		}
	}
}