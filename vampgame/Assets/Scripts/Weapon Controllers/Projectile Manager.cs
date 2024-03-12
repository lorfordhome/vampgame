using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//Base script for all weapon controllers
public class ProjectileManager : MonoBehaviour
{
    //base projectile class
    public GameObject prefab;
    public float speed;
    public float size;
    public float damage;
    public float cooldownDuration;
    float currentCooldown;
    protected FirePoint fp;
    protected virtual void Start()
    {
        fp=FindObjectOfType<FirePoint>();
        currentCooldown = cooldownDuration;
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown-=Time.deltaTime;
        if (currentCooldown<=0)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown = cooldownDuration;
    }
}
