using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimiter = 1.4142f; //root two (we love pythagoras)
    public Animator animator;
    public float runSpeed = 20.0f;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
 
    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
        // animation control. checks if the player is moving or not
        if (horizontal==0 && vertical==0)
        {
            animator.SetBool("walking", false);
        }
        else
        {
            animator.SetBool("walking", true);
        }
        //sets direction of sprite
        if (horizontal == -1)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if (horizontal == 1)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
  
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limits movement speed diagonally, so it isn't any faster 
            horizontal /= moveLimiter;
            vertical /= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        PreventLeavingScreen();
    }

    //wip function
    private void PreventLeavingScreen()
    {
        float mapboundx = GameObject.FindGameObjectWithTag("background").GetComponent<SpriteRenderer>().bounds.size.x;
        if (transform.position.x > mapboundx)
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }
    }

}
