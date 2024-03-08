using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector2 mousePos;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        mousePos=cam.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
