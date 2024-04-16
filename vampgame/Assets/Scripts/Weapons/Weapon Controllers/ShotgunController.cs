using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunController : ProjectileManager
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedProjectile = Instantiate(weaponData.Prefab);
        spawnedProjectile.transform.position = transform.position; //assign the position to be the same as this object 
        spawnedProjectile.transform.rotation = fp.transform.rotation;
        spawnedProjectile.GetComponent<FireBehaviour>().DirectionChecker(fp.projdirection);
    }
}
