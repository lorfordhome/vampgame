using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LightRing : MonoBehaviour
{

    public GameObject Lantern; 
    public float shrinkRate = 0.05f; //how fast the light shrinks
    public float growRate = 0.1f; //how fast the light can grow

    public Vector3 minSize = new Vector3(0.1f, 0.1f, 0.1f); // min size is small enough that you cant see it
    public Vector3 maxSize = new Vector3(2.5f, 2.5f, 2.5f); //set max size as slightly bigger than initial size

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.L) )
        {
            if (transform.localScale != minSize)
            {
                ShrinkLight(shrinkRate);

			}
        }

		if (Input.GetKey(KeyCode.K))
		{
			if (transform.localScale != maxSize)
			{
                GrowLight(growRate);

			}
		}
        ShrinkLight(shrinkRate);
        if (transform.localScale.x <= minSize.x)
        {
            SceneManager.LoadScene("Game_Over");
        }
	}
    public void ShrinkLight(float shrink)
    {
        transform.localScale -= new Vector3(shrinkRate, shrinkRate, shrinkRate);
        transform.localScale = new Vector3(
               Mathf.Clamp(transform.localScale.x, minSize.x, maxSize.x),
               Mathf.Clamp(transform.localScale.y, minSize.y, maxSize.y),
               Mathf.Clamp(transform.localScale.z, minSize.z, maxSize.z));
    }
    public void GrowLight(float growth)
    {
        //grows the lanterns scale uniformly
        transform.localScale += new Vector3(growth, growth, growth);

        //clamping the maximum size
        transform.localScale = new Vector3(
        Mathf.Clamp(transform.localScale.x, minSize.x, maxSize.x),
        Mathf.Clamp(transform.localScale.y, minSize.y, maxSize.y),
        Mathf.Clamp(transform.localScale.z, minSize.z, maxSize.z));
    }
}
