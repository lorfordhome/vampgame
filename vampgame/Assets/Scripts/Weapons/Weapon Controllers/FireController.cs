using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FireController : ProjectileManager
{
    // Start is called before the first frame update
    public float spawnOffset=0.5f;
    public float shootingDistance=8f;
    Transform target;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        target = FindClosestEnemy();
    }

    protected override void Attack()
    {
        if (target != null)
        {
            base.Attack();
            GameObject spawnedProjectile = Instantiate(weaponData.Prefab);
            spawnedProjectile.transform.position = new Vector3(transform.position.x + spawnOffset, transform.position.y + spawnOffset, transform.position.z); //assign the position to be the same as this object 
            Vector2 aimDir = target.position - transform.position;
            spawnedProjectile.GetComponent<Rigidbody2D>().rotation = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg; //calculates angle of enemy relative to the player.
            spawnedProjectile.GetComponent<FireBehaviour>().DirectionChecker(target.transform.position - transform.position);
        }
    }
    public Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // if no enemies found return nothing
        if (enemies.Length == 0)
        {
            Debug.LogWarning("No enemies found!", this);
            return null;
        }

        GameObject closest;

        // if there is only exactly one anyway skip the rest and return it directly
        if (enemies.Length == 1)
        {
            closest = enemies[0];
            return closest.transform;
        }


        // otherwise: Take the enemies
        closest = enemies
            // order them by distance ascending
            .OrderBy(go => (transform.position - go.transform.position).sqrMagnitude)
            .First();

        if (Vector2.Distance(transform.position, closest.transform.position) < shootingDistance)
        {
            return closest.transform;
        }
        else
        {
            return null;
        }
    }

}
