using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickup : Pickups
{
    public int healthGranted = 50;
    public void OnDestroy()
    {
        target.HealDamage(healthGranted);
    }
}
