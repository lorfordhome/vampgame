using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : ProjectileManager
{
    // Start is called before the first frame update
    public float spawnOffset=0.5f;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedProjectile = Instantiate(weaponData.Prefab);
        spawnedProjectile.transform.position = new Vector3(transform.position.x+spawnOffset,transform.position.y+spawnOffset,transform.position.z); //assign the position to be the same as this object 
        spawnedProjectile.transform.rotation = fp.transform.rotation;
        spawnedProjectile.GetComponent<FireBehaviour>().DirectionChecker(fp.projdirection);
    }

}
