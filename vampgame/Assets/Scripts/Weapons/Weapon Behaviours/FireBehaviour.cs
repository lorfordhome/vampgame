using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBehaviour : ProjectileBehaviour
{
    FireController fc;
    protected override void Start()
    {
        base.Start();
        fc=FindObjectOfType<FireController>();
    }

    void Update()
    {
        transform.position += direction * currentSpeed * Time.deltaTime; //set the movement of the fire
    }
}
