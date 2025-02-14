using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeBehaviour : MonoBehaviour

{
    public WeaponScriptableObject weaponData;

    public float destroyAfterSeconds;

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

}