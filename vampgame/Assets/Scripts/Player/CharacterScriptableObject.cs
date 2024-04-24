using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName ="CharacterScriptableObject",menuName ="ScriptableObjects/Character")]


// i made this a scriptable object because at the start of the project we were toying with the idea of having multiple characters
//this was scrapped but it made the character stats easy to modify, and really came in useful when i made the passive items :)
public class CharacterScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject startingWeapon;
    public GameObject StartingWeapom { get => startingWeapon; private set => startingWeapon = value; }
    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    [SerializeField]
    float recovery;
    public float Recovery { get => recovery; private set => recovery = value; }

    [SerializeField]
    float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }

    [SerializeField]
    float might;
    public float Might { get => might; private set => might = value; }

    [SerializeField]
    float projectileSpeed;
    public float ProjectileSpeed { get => projectileSpeed; private set => projectileSpeed = value; }
    [SerializeField]
    float magnet;
    public float Magnet { get => magnet; private set => magnet = value; }

}
