using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
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

    //class for managaging levels. essentially - the higher player lvl, the more exp is needed to lvl up. also used for tracking xp overflow (allowing player to ie. gain multiple levels at once)
    [System.Serializable]//means its fields are visible and editable in the inspector
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }
    public List<LevelRange> levelRanges;
    private void Start()
    {
        //initialises the experience cap
        experienceCap = levelRanges[0].experienceCapIncrease;
        _expbar.UpdateHealthBar(experienceCap, experience);
    }
    public void IncreaseExperience(int amount)
    {
        experience += amount;
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
        }
        Debug.Log(experience);
        Debug.Log(experienceCap);
        _expbar.UpdateHealthBar(experienceCap, experience);
    }

    public void TakeDamage(float dmg)
    {
 
        if (!isInvincible)
        {
            currentHealth -= dmg;
            _healthbar.UpdateHealthBar(characterData.MaxHealth, currentHealth);
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
            if (currentHealth <= 0)
            {
                Kill();
            }
        }

    }
    public void Kill()
    {
        Debug.Log("PLAYER DEAD");
        SceneManager.LoadScene("Game_Over");
    }
    private void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        //if the invincibility timer has reached 0, end invincibility
        else if (isInvincible)
        {
            isInvincible = false;
        }
    }
    private void Awake()
    {
        //assign the values
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        _healthbar.UpdateHealthBar(characterData.MaxHealth, currentHealth);
    }
}
