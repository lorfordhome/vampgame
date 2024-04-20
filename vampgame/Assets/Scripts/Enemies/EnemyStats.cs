using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;

    public GameObject particleExplosion;

    public float despawnDistance = 20f;
    Transform player;

    [Header("Damage Feedback")]
    public Color damageColor = new Color(1, 0, 0, 1);
    public float damageFlashDuration = 0.2f;
    public float deathFadeTime = 0.6f;
    Color originalcolor;
    SpriteRenderer spriteRenderer;
    EnemyMovement movement;

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        spriteRenderer= GetComponent<SpriteRenderer>();
        originalcolor = spriteRenderer.color;
        movement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        } 
    }
    private void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }
    public void TakeDamage(float dmg,Vector2 sourcePosition,float knockbackForce=10f,float knockbackDuration=0.2f)
    {
        currentHealth -= dmg;
        StartCoroutine(DamageFlash());
        if (knockbackForce > 0)
        {
            Vector2 dir=(Vector2)transform.position-sourcePosition;
            movement.Knockback(dir.normalized*knockbackForce, knockbackDuration);
        }
        if (currentHealth <= 0)
        {
            Kill();
        }
    }
    public void Kill()
    {
        Destroy(gameObject);
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        //checks if the collision is with the player
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
            this.TakeDamage(0f, this.transform.position,5f,0.1f);
        }
    }

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded)//stops that annoying error message
        {
            return;
        }
        Instantiate(particleExplosion, transform.position, Quaternion.identity);
        EnemySpawner es=FindObjectOfType<EnemySpawner>();
        es.OnEnemyKilled();
    }

    void ReturnEnemy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)].position;
    }

    IEnumerator DamageFlash()
    {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(damageFlashDuration);
        spriteRenderer.color = originalcolor;
    }
}
