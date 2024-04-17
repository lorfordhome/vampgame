using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class LanternFuel : MonoBehaviour
{
    public GameObject Fuel;
    public GameObject Player;
    private bool fuelPickedUp;


    // Start is called before the first frame update
    void Start()
    {
        fuelPickedUp = false;
    }

	void OnTriggerEnter(Collider other)
	{
		// Check if the player collided with the collectible
		if (other.tag == "Player")
        {
            Debug.Log("pickup fuel");
            Destroy(gameObject);

        }
	}
}
