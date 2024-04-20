using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpGem : MonoBehaviour, I_collectible
{
    public int experienceGranted;
    public void Collect()
    {
        PlayerStats player=FindObjectOfType<PlayerStats>();
        player.IncreaseExperience(experienceGranted);
        Destroy(gameObject);
        Debug.Log("EXP Collected");
    }
}
