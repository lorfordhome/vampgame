using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Base script of all projectile behaviours. To be placed on a prefab of a projectil weapon.
public class ProjectileBehaviour : MonoBehaviour
{
    protected Vector3 direction;
    public float destroyAfterSeconds;
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;
    }
}
