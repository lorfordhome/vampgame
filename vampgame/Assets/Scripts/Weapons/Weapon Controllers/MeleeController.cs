
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : ProjectileManager
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedMelee = Instantiate(weaponData.Prefab);
        spawnedMelee.transform.position = transform.position; //Assign the position to be the same as this object which is parented to the player
        spawnedMelee.transform.parent = transform;
    }
}