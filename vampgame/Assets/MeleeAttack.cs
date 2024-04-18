using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float meleeSpeed;
    [SerializeField] private float damage;
    public SwordController controller;
    float timeUntilMelee;
    public AudioSource adSource;
    public AudioClip[] adClips;
    private int adCount = 0;

    private void Start()
    {
        controller=FindObjectOfType<SwordController>();
        adSource=GetComponent<AudioSource>();
    }

    private void PlayAudio()
    {
        adSource.clip = adClips[adCount];
        adSource.Play();
        adCount++;
        if (adCount > 2)
        {
            adCount = 0;
        }
    }
    private void Update()
    {
        if (timeUntilMelee <= 0f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                controller.canRotate = false;
                anim.SetTrigger("Attack");
                PlayAudio();
                timeUntilMelee = meleeSpeed;
            }
        }
        else
        {
            timeUntilMelee -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if (collision.CompareTag("Enemy"))
        {
            EnemyStats enemy = collision.GetComponent<EnemyStats>();
            enemy.TakeDamage(damage);
        }
    }
    void AnimDone()
    {
        controller.canRotate = true;
    }
}
