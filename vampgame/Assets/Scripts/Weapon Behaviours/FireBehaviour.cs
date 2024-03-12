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
        transform.position += direction * fc.speed * Time.deltaTime; //set the movement of the knife
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="Collidable")
        {
            Debug.Log("collision");
            this.gameObject.SetActive(false);
        }
    }
}
