using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Base script of all projectile behaviours. To be placed on a prefab of a projectil weapon.
public class ProjectileBehaviour : MonoBehaviour
{
    protected Vector3 direction;
    public WeaponScriptableObject weaponData;
    public float destroyAfterSeconds;

    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;

    private void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed=weaponData.Speed;
        currentCooldownDuration=weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;
    }
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
            ReducePierce();

        }
        if (col.CompareTag("Collidable"))
        {
            this.gameObject.SetActive(false);
        }
    }
    void ReducePierce()//destroy once the pierce reaches 0
    {
        currentPierce--;
        if (currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
