using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LightRing : MonoBehaviour
{

    public GameObject Lantern; 
    public float shrinkRate = 0.01f; //how fast the light shrinks
    public float growRate = 0.1f; //how fast the light can grow

    public Vector3 minSize = new Vector3(0.1f, 0.1f, 0.1f); // min size is small enough that you cant see it
    public Vector3 maxSize = new Vector3(2.5f, 2.5f, 2.5f); //set max size as slightly bigger than initial size

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.L) )
        {
            if (transform.localScale != minSize)
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

		if (Input.GetKey(KeyCode.K))
		{
			if (transform.localScale != maxSize)
			{
				Transform transform = Lantern.transform;

				//grows the lanterns scale uniformly
				transform.localScale += new Vector3(growRate, growRate, growRate);

				//clamping the maximum size
				transform.localScale = new Vector3(
				Mathf.Clamp(transform.localScale.x, minSize.x, maxSize.x),
				Mathf.Clamp(transform.localScale.y, minSize.y, maxSize.y),
				Mathf.Clamp(transform.localScale.z, minSize.z, maxSize.z));

			}
		}
	}
}
