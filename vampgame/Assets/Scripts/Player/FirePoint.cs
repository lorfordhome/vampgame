using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    Vector2 mousePos;
    public Camera cam;
    public Rigidbody2D body;
    public Vector3 projdirection;
    public float angle;

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);//gets mouse position
        Vector2 aimDir = mousePos - body.position;
        angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg; //calculates angle of mouse relative to the player.
        body.rotation = angle;//points object towards mouse. this is needed for aiming
        projdirection = new Vector3(mousePos.x, mousePos.y, 0) - transform.position;//the direction any spawned projectiles shoud move in
        projdirection = projdirection.normalized; //otherwise magnitude is dependent on the distance of the mouse from the object
    }
}
