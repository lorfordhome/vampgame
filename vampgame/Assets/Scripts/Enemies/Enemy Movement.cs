using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    EnemyStats enemy;
    SpriteRenderer spriteRenderer;
    float oldPosition;
    Vector2 knockbackVelocity;
    public float knockbackDuration;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        enemy=GetComponent<EnemyStats>();
        spriteRenderer=GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (knockbackDuration > 0)
        {
            transform.position += (Vector3)knockbackVelocity * Time.deltaTime;
            knockbackDuration -= Time.deltaTime;
        }
        else
        {
            //constantly move enemy towards player
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime);
            if (transform.position.x > oldPosition) // they are not the same direction
                spriteRenderer.flipX = false;
            else if (transform.position.x < oldPosition)
                spriteRenderer.flipX = true;
        }

    }
    private void LateUpdate()
    {
        oldPosition = transform.position.x;
    }

    public void Knockback(Vector2 velocity, float duration)
    {
        if (knockbackDuration > 0) return;
        //begin knockback
        knockbackVelocity = velocity;
        knockbackDuration = duration;
    }
}
