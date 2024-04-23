using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimiter = 1.4142f; //root two (we love pythagoras)
    public Animator animator;
    public GameObject firePoint;
    PlayerStats player;
    public AudioSource walkingSound;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        player=GetComponent<PlayerStats>();

    }

    void Update()
    {
        walkingSound.mute = false;
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
        // animation control. checks if the player is moving or not
        if (horizontal==0 && vertical==0)
        {
            animator.SetBool("walkingleft", false);
            animator.SetBool("walkingright", false);
            animator.SetBool("walkingup", false);
            animator.SetBool("walkingdown", false);
            walkingSound.mute = true;
        }
        //sets direction of sprite
        if (horizontal == -1)
        {
            animator.SetBool("walkingright", false);
            animator.SetBool("walkingleft", true);
        }
        if (horizontal == 1)
        {
            animator.SetBool("walkingleft", false);
            animator.SetBool("walkingright", true);
        }
        if (vertical == -1)
        {
            animator.SetBool("walkingdown", true);
            animator.SetBool("walkingup", false);
        }
        if (vertical == 1)
        {
            animator.SetBool("walkingdown", false);
            animator.SetBool("walkingup", true);
        }
        GetComponent<Rigidbody2D>().position = body.position;
        firePoint.transform.position = body.position;
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limits movement speed diagonally, so it isn't any faster 
            horizontal /= moveLimiter;
            vertical /= moveLimiter;
        }
   
        body.velocity = new Vector2(horizontal * player.currentMoveSpeed, vertical * player.currentMoveSpeed);
    }

}
