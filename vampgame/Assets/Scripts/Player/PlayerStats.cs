using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public Camera camera;
    [SerializeField] private Healthbar _healthbar;
    [SerializeField] private Healthbar _expbar;
    public CharacterScriptableObject characterData;
    //current stats
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentMight;
    [HideInInspector]
    public float currentProjectileSpeed;
    [HideInInspector]
    public float currentMagnet;

    //experience and levelling
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    //I-frames
    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    [Header("Damage Feedback")]
    public Color damageColour = new Color(1, 0, 0, 1);
    public float damageFlashDuration = 0.2f;
    private SpriteRenderer spriteRenderer;
    Color originalcolour;

    //sounds
    public AudioSource adSource;
    public AudioClip[] adClips;
    private int adCount = 0;
    private float soundTimer = 0f;
    public float soundCooldown = 1.2f;

    //class for managaging levels. essentially - the higher player lvl, the more exp is needed to lvl up. also used for tracking xp overflow (allowing player to ie. gain multiple levels at once)
    [System.Serializable]//means its fields are visible and editable in the inspector
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }
    public List<LevelRange> levelRanges;

    InventoryManager inventory;
    public int weaponIndex;
    public int passiveIndex;

    private void PlayAudio()
    {
        adSource.clip = adClips[adCount];
        adSource.Play();
        adCount++;
        if (adCount > 1)
        {
            adCount = 0;
        }
    }
    private void Start()
    {
        //initialises the experience cap
        experienceCap = levelRanges[0].experienceCapIncrease;
        _expbar.UpdateHealthBar(experienceCap, experience);
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalcolour = spriteRenderer.color;
    }
    public void IncreaseExperience(int amount)
    {
        experience += amount;
        _expbar.UpdateHealthBar(experienceCap, experience);
        LevelUpChecker();
    }
    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            Debug.Log("Level up!");
            level++;
            experience -= experienceCap;
            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
            GameManager.instance.StartLevelUp();
        }
        Debug.Log(experience);
        Debug.Log(experienceCap);
        _expbar.UpdateHealthBar(experienceCap, experience);

    }

    public void TakeDamage(float dmg)
    {
        StartCoroutine(DamageFlash());
 
        if (!isInvincible)
        {
            camera.GetComponent<ShakeCamera>().TriggerShake(0.2f);
            currentHealth -= dmg;
            _healthbar.UpdateHealthBar(characterData.MaxHealth, currentHealth);
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
            if(soundTimer<= 0)
            {
                PlayAudio();
                soundTimer = soundCooldown;
            }
            if (currentHealth <= 0)
            {
                Kill();
            }
        }

    }
    public void HealDamage(float heal)
    {
        currentHealth+= heal;
        _healthbar.UpdateHealthBar(characterData.MaxHealth, currentHealth);
        invincibilityTimer = invincibilityDuration;
        isInvincible = true;
    }
    public void Kill()
    {
        Debug.Log("PLAYER DEAD");
        Cursor.visible = true;
        SceneController.instance.ChangeScene("Game_Over");
    }
    private void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible)
        {
                isInvincible = false;
        }
        if (soundTimer > 0)
        {
            soundTimer-= Time.deltaTime;
        }
        //if the invincibility timer has reached 0, end invincibility
       
    }
    private void Awake()
    {
        //assign the values
        inventory=GetComponent<InventoryManager>();
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentMagnet=characterData.Magnet;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        _healthbar.UpdateHealthBar(characterData.MaxHealth, currentHealth);
    }

    public void SpawnWeapon(GameObject weapon)
    {
        if (weaponIndex >= inventory.weaponSlots.Count)
        {
            Debug.LogError("Inventory full");
            return;
        }
        //spawn starting weapon
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<ProjectileManager>());
        weaponIndex++;
    }
    public void SpawnPassive(GameObject passive)
    {

        if (passiveIndex >= inventory.passiveSlots.Count)
        {
            Debug.LogError("Inventory full");
            return;
        }
        //spawn starting weapon
        GameObject spawnedPassive = Instantiate(passive, transform.position, Quaternion.identity);
        spawnedPassive.transform.SetParent(transform);
        inventory.AddPassive(weaponIndex, spawnedPassive.GetComponent<PassiveItem>());
        passiveIndex++;
    }
    IEnumerator DamageFlash()
    {
        spriteRenderer.color = damageColour;
        yield return new WaitForSeconds(damageFlashDuration);
        spriteRenderer.color = originalcolour;
    }
}
