using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    EnemyStats enemy;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        enemy=GetComponent<EnemyStats>();
    }

    void Update()
    {
        //constantly move enemy towards player
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime); 
    }
}
