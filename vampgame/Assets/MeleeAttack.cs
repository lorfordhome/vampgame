using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] public Animator anim;
    public SwordController controller;
    float timeUntilMelee;
    public AudioSource adSource;
    public AudioClip[] adClips;
    private int adCount = 0;

    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;

    public float GetCurrentDamage()
    {
        return currentDamage * FindObjectOfType<PlayerStats>().currentMight;
    }
    private void Start()
    {
        controller=FindObjectOfType<SwordController>();
        adSource=GetComponent<AudioSource>();
        currentDamage = controller.swordData.Damage;
        currentSpeed = controller.swordData.Speed;
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

    private void FindSwordAnim()
    {
        Debug.Log("looking for sword");
        foreach (Transform transform in controller.transform)
        {
            if (transform.CompareTag("sword"))
            {
                Debug.Log("FOUND SWORD");
                anim = transform.gameObject.GetComponent<Animator>();
                break;
            }
        }
    }
    private void Update()
    {
        if (controller == null || anim==null)
        {
            controller = FindObjectOfType<SwordController>();
            FindSwordAnim();
        }
        if (timeUntilMelee <= 0f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Attack");
                PlayAudio();
                timeUntilMelee = currentSpeed;
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
            enemy.TakeDamage(GetCurrentDamage(), transform.position);
        }
    }
}
