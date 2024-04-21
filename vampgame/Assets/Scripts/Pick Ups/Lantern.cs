using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : Pickups
{
    public float lightGiven=10f;
    public void OnDestroy()
    {
        LightRing light = FindObjectOfType<LightRing>();
        light.GrowLight(lightGiven);
        Debug.Log("light Collected");
    }
}
