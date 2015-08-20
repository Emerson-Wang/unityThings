using UnityEngine;
using System.Collections;

public class mobSize : MonoBehaviour {

	private float mobAOE;
	public int size;
	public GameObject Baraccuda;
	// Use this for initialization
	void Start () 
	{
		mobAOE = Baraccuda.GetComponent<BaraccudaFire>().AOE;
		mobAOE -= 5f;
		size = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Collider[] mobCheck = Physics.OverlapSphere(gameObject.transform.position, mobAOE);
		int tempSize = 0;
		foreach(Collider mob in mobCheck)
		{
			if(mob.CompareTag ("Dwarf"))
			{
				tempSize += 1;
			}
		}
		size = tempSize;
	}
}
