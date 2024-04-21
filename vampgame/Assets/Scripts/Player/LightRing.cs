using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LightRing : MonoBehaviour
{
	public GameObject Fuel;     //Fuel is the "LanternFuel" game object
    public GameObject Lantern;  //Latern is the "LampLight" game object


    public float shrinkRate;	//how fast the light shrinks (set in inspector)
    public float growRate;      //how fast the light can grow  (set in inspector)
	public float refuelTime = 2f;	//how long the lantern will stay at full size after refuelling


    public Vector3 minSize = new Vector3(0.1f, 0.1f, 0.1f); // min size is small enough that you cant see it
    public Vector3 maxSize = new Vector3(3f, 3f, 3f);		//set max size as slightly bigger than initial size

	private bool Growing = false;


	//Refuel stores the logic for the light ring growing
	private IEnumerator Refuel()	//set as coroutine (IEnumerator) to be able to set a pause before setting Growing as false
	{
		
		
		if (transform.localScale != maxSize)	//cant pickup another fuel item whilst at full size
		{		
			/*Ring of light growing code*/
			Transform transform = Lantern.transform;

			//grows the lanterns scale uniformly
			transform.localScale = maxSize;	//sets the light ring to max size

			//clamping the maximum size
			transform.localScale = new Vector3(
			Mathf.Clamp(transform.localScale.x, minSize.x, maxSize.x),
			Mathf.Clamp(transform.localScale.y, minSize.y, maxSize.y),
			Mathf.Clamp(transform.localScale.z, minSize.z, maxSize.z));

			//fuelPickedUp = false;
			yield return new WaitForSeconds(refuelTime);
			Debug.Log("2 Seconds");
			Growing = false;
								
		}
		
	}

	
	// Update is called once per frame
	void Update()
    {
		
		if (Growing)
		{
			StartCoroutine(Refuel());
			
		}
		if (transform.localScale != minSize && !Growing) 
		{
	
			/*Ring of light shrinking code*/
			Transform transform = Lantern.transform;

			//shrinks the lanterns scale uniformly
			transform.localScale -= new Vector3(shrinkRate, shrinkRate, shrinkRate);

			//clamping the minimum size
			transform.localScale = new Vector3(
			Mathf.Clamp(transform.localScale.x, minSize.x, maxSize.x),
			Mathf.Clamp(transform.localScale.y, minSize.y, maxSize.y),
			Mathf.Clamp(transform.localScale.z, minSize.z, maxSize.z));
			
		}
	}

		

	private void OnTriggerEnter2D(Collider2D other)
	{
		/*player x fuel collision code*/	
		
		if (other.tag == "Fuel")		// Check if the Player/LightRing's collider has entered the Fuel's
		{
			Growing = true;
			Debug.Log("pickup fuel");	
			Destroy(Fuel);				//gets rid of pickup
			
		}
	}
}
