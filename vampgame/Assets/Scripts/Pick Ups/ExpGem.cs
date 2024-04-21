using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpGem : Pickups
{
    public int experienceGranted=25;
    public void OnDestroy()
    {
        target.IncreaseExperience(experienceGranted);
    }
}
