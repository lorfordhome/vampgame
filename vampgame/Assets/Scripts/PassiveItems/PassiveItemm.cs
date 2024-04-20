using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    protected PlayerStats player;
    public PassiveItemScriptableObject passiveData;

    protected virtual void ApplyModifier()
    {
        //insert appropriate value in the child classes
    }
   
    void Start()
    {
        player=FindObjectOfType<PlayerStats>();
        ApplyModifier();
    }

}
