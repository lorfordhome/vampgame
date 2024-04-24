using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunController : ProjectileManager
{

    public float spawnOffset = 0.5f;//so it doesnt spawn directly on top of the player
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedProjectile = Instantiate(weaponData.Prefab);
        spawnedProjectile.transform.position = new Vector3(transform.position.x + spawnOffset, transform.position.y + spawnOffset, transform.position.z); //assign the position to be the same as this object 
        spawnedProjectile.transform.rotation = fp.transform.rotation;
        spawnedProjectile.GetComponent<FireBehaviour>().DirectionChecker(fp.projdirection);
    }
}
