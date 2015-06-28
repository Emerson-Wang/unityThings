using UnityEngine;
using System.Collections;

public class buildingPlacement : MonoBehaviour {

	public GameObject buildingSub;
	private GameObject building;
	private Vector3 surfacePlace;
	private Vector3 buildDimensions;
	private float yOffset;
	private Vector3 plusY;
	private Touch touchPlace;
	private Collider[] spaceCheck;
	public GameObject otherBuilding;

	//Checking for input to fire cannonball
	void Update()
	{
		// When left mouse button is pressed, executes "Shoot" function
		if(Input.GetButtonDown ("Fire1"))
		{
			MousePlacement();
		}
		else if(Input.touchCount > 0)
		{
			TouchPlacement();
		}
		
	}
	
	void MousePlacement()
	{	
		//Raycast to find out where the building should be placed
		RaycastHit placement;
		//Finding mouse position when left mouse button is pressed
		Vector3 mousePlace = Input.mousePosition;
		//Translating screen position of mouse to world position on the field.
		Ray mouseClick = Camera.main.ScreenPointToRay(mousePlace);

		//If the click actually lands on the field it will fire the cannonball
		if (Physics.Raycast(mouseClick, out placement))
		{
			//Finding how tall the building is to place it on the surface of the ground
			buildDimensions = buildingSub.GetComponent<Renderer>().bounds.size;
			yOffset = (buildDimensions.y/2) + placement.point.y;
			//Calculating height to have buildling resting on surface of ground
			surfacePlace = new Vector3(((int)(placement.point.x/10))*10, yOffset, ((int)(placement.point.z/10))*10);
			//Checking if the new building overlaps any other buildings; terminates if true
			spaceCheck = Physics.OverlapSphere(surfacePlace, buildDimensions.x/2);
			foreach(Collider other in spaceCheck)
			{
				GameObject otherBuilding = other.gameObject;
				if(otherBuilding.gameObject.CompareTag("Building"))
				{
					return;
				}
			}
			//Placing building on surface
			building = Instantiate(buildingSub, surfacePlace, Quaternion.identity) as GameObject;

		}
	}

	void TouchPlacement()
	{	
		//Raycast to find out where the building should be placed
		RaycastHit placement;
		//Finding mouse position when left mouse button is pressed
		touchPlace = Input.GetTouch(0);
		//Translating screen position of mouse to world position on the field.
		Ray touch = Camera.main.ScreenPointToRay(touchPlace.position);
			
		//If the click actually lands on the field it will fire the cannonball
		if (Physics.Raycast(touch, out placement))
		{
			//Finding how tall the building is to place it on the surface of the ground
			buildDimensions = buildingSub.GetComponent<Renderer>().bounds.size;
			yOffset = (buildDimensions.y/2) + placement.point.y;
			//Calculating height to have buildling resting on surface of ground
			surfacePlace = new Vector3(((int)(placement.point.x/10))*10, yOffset, ((int)(placement.point.z/10))*10);
			//Checking if the new building overlaps any other buildings; terminates if true
			spaceCheck = Physics.OverlapSphere(surfacePlace, buildDimensions.x/2);
			foreach(Collider other in spaceCheck)
			{
				GameObject otherBuilding = other.gameObject;
				if(otherBuilding.gameObject.CompareTag("Building"))
				{
					return;
				}
			}
			//Placing building on surface
			building = Instantiate(buildingSub, surfacePlace, Quaternion.identity) as GameObject;
		}

	}

}
