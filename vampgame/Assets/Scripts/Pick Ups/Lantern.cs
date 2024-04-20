using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour, I_collectible
{
    public float lightGiven;
    public void Collect()
    {
        LightRing light = FindObjectOfType<LightRing>();
        light.GrowLight(lightGiven);
        Destroy(gameObject);
        Debug.Log("light Collected");
    }
}
