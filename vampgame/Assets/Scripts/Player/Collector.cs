using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        //checks if the other object is a collectible
        if (col.gameObject.TryGetComponent(out I_collectible collectible))
        {
            collectible.Collect();
        }
    }
}
