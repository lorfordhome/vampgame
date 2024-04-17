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
    public float growRate;		//how fast the light can grow  (set in inspector)


    public Vector3 minSize = new Vector3(0.1f, 0.1f, 0.1f); // min size is small enough that you cant see it
    public Vector3 maxSize = new Vector3(3f, 3f, 3f);		//set max size as slightly bigger than initial size

	private bool fuelPickedUp;
	private bool Growing;



	private void Start()
	{
		fuelPickedUp = false;   //boolean set as false on game start
		Growing = false;	
	}

	//Refuel stores the logic for the light ring growing
	private void Refuel()
	{
		/*Ring of light growing code*/
		
		if (transform.localScale != maxSize && fuelPickedUp)
		{	
			Growing = true;
			while (Growing)
			{
				Transform transform = Lantern.transform;

				//grows the lanterns scale uniformly
				transform.localScale += new Vector3(growRate, growRate, growRate);

				//clamping the maximum size
				transform.localScale = new Vector3(
				Mathf.Clamp(transform.localScale.x, minSize.x, maxSize.x),
				Mathf.Clamp(transform.localScale.y, minSize.y, maxSize.y),
				Mathf.Clamp(transform.localScale.z, minSize.z, maxSize.z));

				fuelPickedUp = false;
			}
		}
		
	}

	
	// Update is called once per frame
	void Update()
    {
		/*Ring of light shrinking code*/
		while (!Growing)
		{
			if (transform.localScale != minSize && !fuelPickedUp)
			{
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
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		/*player x fuel collision code*/	
		
		if (other.tag == "Fuel")		// Check if the Player/LightRing's collider has entered the Fuel's
		{
			Debug.Log("pickup fuel");
			fuelPickedUp = true;
			Invoke("Refuel", 2f);		//calls the Refuel function for 2 seconds		
			Destroy(Fuel);
			Growing	= false;
		}
	}
}
