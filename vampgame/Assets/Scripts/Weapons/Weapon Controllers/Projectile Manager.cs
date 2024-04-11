using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//Base script for all weapon controllers
public class ProjectileManager : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    protected FirePoint fp;
    float currentCooldown;
    AudioSource soundEffect;
    protected virtual void Start()
    {
        fp=FindObjectOfType<FirePoint>();
        currentCooldown = weaponData.CooldownDuration;
        soundEffect = GetComponent<AudioSource>();
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
        currentCooldown = weaponData.CooldownDuration;
        soundEffect.Play();
    }
}
