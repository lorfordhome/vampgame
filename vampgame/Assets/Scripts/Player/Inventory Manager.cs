using JetBrains.Annotations;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    public List<ProjectileManager> weaponSlots = new List<ProjectileManager>(2);
    public int[] weaponLevels = new int[5];
    public List<UnityEngine.UI.Image> weaponUIslots=new List<UnityEngine.UI.Image>(2);

    public List<PassiveItem> passiveSlots = new List<PassiveItem>(2);
    public int[] passiveLevels = new int[5];

    public int swordLevel;
    public SwordController sword;

    [System.Serializable]
    public class WeaponUpgrade
    {
        public int weaponUpgradeIndex;
        public GameObject initialWeapon;
        public WeaponScriptableObject weaponData;
    }

    [System.Serializable]
    public class PassiveUpgrade
    {
        public int passiveUpgradeIndex;
        public GameObject initialPassive;
        public PassiveItemScriptableObject passiveData;
    }
    [System.Serializable]
    public class SwordUpgrade
    {
        public GameObject initialSword;
        public SwordScriptableObject swordData;
    }
    [System.Serializable]
    public class UpgradeUI
    {
        public TextMeshProUGUI upgradeNameDisplay;
        public TextMeshProUGUI upgradeDescriptionDisplay;
        public UnityEngine.UI.Image upgradeIcon;
        public UnityEngine.UI.Button upgradeButton;
    }

    public List<WeaponUpgrade> weaponUpgradeOptions = new List<WeaponUpgrade>();
    public List<PassiveUpgrade> passiveUpgradeOptions = new List<PassiveUpgrade>();
    public List<SwordUpgrade> swordUpgradeOptions = new List<SwordUpgrade>();
    public List<UpgradeUI> upgradeUIOptions=new List<UpgradeUI>();
    PlayerStats player;

    private void Start()
    {
        player=GetComponent<PlayerStats>();
    }
    public void AddWeapon(int slotindex, ProjectileManager weapon)
    {
        weaponSlots[slotindex] = weapon;
        weaponLevels[slotindex] = weapon.weaponData.Level;
        weaponUIslots[slotindex].sprite = weapon.weaponData.Icon;
        weaponUIslots[slotindex].enabled = true;
        if (GameManager.instance != null && GameManager.instance.choosingUpgrade)
        {
            GameManager.instance.EndLevelUp();
        }
    }
    public void AddPassive(int slotindex, PassiveItem passive)
    {
        passiveSlots[slotindex] = passive;
        passiveLevels[slotindex] = passive.passiveData.Level;
        if (GameManager.instance != null && GameManager.instance.choosingUpgrade)
        {
            GameManager.instance.EndLevelUp();
        }
    }
    public void AddSword(SwordController swordUpgrade)
    {
        swordLevel=swordUpgrade.swordData.Level;
        sword = swordUpgrade;
        if (GameManager.instance != null && GameManager.instance.choosingUpgrade)
        {
            GameManager.instance.EndLevelUp();
        }
    }

    public void LevelUpWeapon(int slotindex, int upgradeIndex)
    {
        if (weaponSlots.Count > slotindex)
        {
            ProjectileManager weapon = weaponSlots[slotindex];
            if (!weapon.weaponData.NextLevelPrefab)
            {
                Debug.LogError("NO NEXT LEVEL FOR" + weapon.name);
            }
            GameObject upgradedWeapon=Instantiate(weapon.weaponData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedWeapon.transform.SetParent(transform);
            AddWeapon(slotindex,upgradedWeapon.GetComponent<ProjectileManager>());
            Destroy(weapon.gameObject);
            weaponLevels[slotindex] = upgradedWeapon.GetComponent<ProjectileManager>().weaponData.Level;

            weaponUpgradeOptions[upgradeIndex].weaponData = upgradedWeapon.GetComponent<ProjectileManager>().weaponData;
                GameManager.instance.EndLevelUp();
        }
    }
    public void LevelUpPassive(int slotindex, int upgradeIndex)
    {
        if (passiveSlots.Count > slotindex)
        {
            PassiveItem passive = passiveSlots[slotindex];
            if (!passive.passiveData.NextLevelPrefab)
            {
                Debug.LogError("NO NEXT LEVEL FOR" + passive.name);
            }
            GameObject upgradedPassive = Instantiate(passive.passiveData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedPassive.transform.SetParent(transform);
            AddPassive(slotindex, upgradedPassive.GetComponent<PassiveItem>());
            Destroy(passive.gameObject);
            passiveLevels[slotindex] = upgradedPassive.GetComponent<PassiveItem>().passiveData.Level;
            passiveUpgradeOptions[upgradeIndex].passiveData = upgradedPassive.GetComponent<PassiveItem>().passiveData;
                GameManager.instance.EndLevelUp();
        }
    }
    public void LevelUpSword()
    {
        GameObject upgradedSword=Instantiate(sword.swordData.NextLevelPrefab,sword.transform.position, Quaternion.identity);
        upgradedSword.transform.SetParent(transform);
        upgradedSword.transform.localScale=Vector3.one;
        upgradedSword.name = "Sword";
        Destroy(sword.gameObject);
        AddSword(upgradedSword.GetComponent<SwordController>());
        swordLevel =upgradedSword.GetComponent<SwordController>().swordData.Level;
        if (swordLevel >= 6)
        {
            sword.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
            GameManager.instance.EndLevelUp();

    }

    int GenerateUpgradeOption()
    {
        return Random.Range(1, 4);
    }
    void ApplyUpgradeOptions()
    {
        List<WeaponUpgrade> availableWeaponUpgrades = new List<WeaponUpgrade>(weaponUpgradeOptions);
        List<PassiveUpgrade> availablePassiveUpgrades = new List<PassiveUpgrade>(passiveUpgradeOptions);
        List<SwordUpgrade> availableSwordUpgrades = new List<SwordUpgrade>(swordUpgradeOptions);
        foreach(var upgradeOption in upgradeUIOptions)
        {
            if (availableWeaponUpgrades.Count == 0 && availablePassiveUpgrades.Count == 0 && availableSwordUpgrades.Count == 0)
            {
                return;
            }
            bool validType = false;
            int upgradeType=1;
            while (!validType)
            {
                upgradeType = GenerateUpgradeOption();
                if (availableWeaponUpgrades.Count == 0 && upgradeType == 1)
                {
                    Debug.Log("no weapons left");
                    continue;
                }
                else if (availablePassiveUpgrades.Count == 0 && upgradeType == 2)
                {
                    Debug.Log("no passives left");
                    continue;
                }
                else if (availableSwordUpgrades.Count == 0 && upgradeType == 3)
                {
                    Debug.Log("no swords left");
                    continue;
                }
                else
                {
                    validType= true;
                }
            }

            if (upgradeType == 1)
            {
                WeaponUpgrade chosenWeaponUpgrade = availableWeaponUpgrades[Random.Range(0, availableWeaponUpgrades.Count)];
                availableWeaponUpgrades.Remove(chosenWeaponUpgrade);
                if (chosenWeaponUpgrade != null)//prevent duplication
                {
                    EnableUpgradeUI(upgradeOption);
                    bool newWeapon = false;
                    for (int i = 0; i < weaponSlots.Count; ++i)
                    {
                        if (weaponSlots[i] != null && weaponSlots[i].weaponData == chosenWeaponUpgrade.weaponData)
                        {
                            newWeapon = false;
                            if (!newWeapon)
                            {
                                if (!chosenWeaponUpgrade.weaponData.NextLevelPrefab)
                                {
                                    DisableUpgradeUI(upgradeOption);
                                    break;
                                }
                                upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpWeapon(i, chosenWeaponUpgrade.weaponUpgradeIndex)); //APPLY BUTTON FUNCTIONALITY
                                //sets it to be that of the next level
                                upgradeOption.upgradeDescriptionDisplay.text = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<ProjectileManager>().weaponData.Description;
                                upgradeOption.upgradeNameDisplay.text = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<ProjectileManager>().weaponData.Name;
                            }
                            break;
                        }
                        else
                        {
                            newWeapon = true;
                        }
                    }
                    if (newWeapon)//give the player the weapon
                    {
                        upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnWeapon(chosenWeaponUpgrade.initialWeapon));//APPLY BUTTON FUNCTIONALITY
                        upgradeOption.upgradeDescriptionDisplay.text = chosenWeaponUpgrade.weaponData.Description;
                        upgradeOption.upgradeNameDisplay.text = chosenWeaponUpgrade.weaponData.Name;
                    }
                    upgradeOption.upgradeIcon.sprite = chosenWeaponUpgrade.weaponData.Icon;

                }
            }
            else if (upgradeType == 2)
            {
                PassiveUpgrade chosenPassiveUpgrade = availablePassiveUpgrades[Random.Range(0, availablePassiveUpgrades.Count)];
                availablePassiveUpgrades.Remove(chosenPassiveUpgrade);
                if (chosenPassiveUpgrade != null)
                {
                    EnableUpgradeUI(upgradeOption);
                    bool newPassive = false;
                    for (int i = 0; i < passiveSlots.Count; ++i)
                    {
                        if (passiveSlots[i] != null && passiveSlots[i].passiveData == chosenPassiveUpgrade.passiveData)
                        {
                            newPassive = false;
                            if (!newPassive)
                            { 
                                if (!chosenPassiveUpgrade.passiveData.NextLevelPrefab)
                                {
                                    DisableUpgradeUI(upgradeOption);
                                    break;
                                }
                                upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpPassive(i,chosenPassiveUpgrade.passiveUpgradeIndex));
                                upgradeOption.upgradeDescriptionDisplay.text = chosenPassiveUpgrade.passiveData.NextLevelPrefab.GetComponent<PassiveItem>().passiveData.Description;
                                upgradeOption.upgradeNameDisplay.text = chosenPassiveUpgrade.passiveData.NextLevelPrefab.GetComponent<PassiveItem>().passiveData.Name;
                            }
                            break;
                        }
                        else
                        {
                            newPassive = true;
                        }
                    }
                    if (newPassive)
                    {
                        upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnPassive(chosenPassiveUpgrade.initialPassive));
                        upgradeOption.upgradeDescriptionDisplay.text = chosenPassiveUpgrade.passiveData.Description;
                        upgradeOption.upgradeNameDisplay.text=chosenPassiveUpgrade.passiveData.Name;
                    }
                    upgradeOption.upgradeIcon.sprite = chosenPassiveUpgrade.passiveData.Icon;
                }
            }
            else if (upgradeType == 3)
            {
                SwordUpgrade chosenSwordUpgrade = availableSwordUpgrades[Random.Range(0, availableSwordUpgrades.Count)];
                availableSwordUpgrades.Remove(chosenSwordUpgrade);
                if (chosenSwordUpgrade != null)
                {
                    EnableUpgradeUI(upgradeOption);
                    if (!chosenSwordUpgrade.swordData.NextLevelPrefab)
                    {
                        break;
                    }
                    upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpSword());
                    upgradeOption.upgradeIcon.sprite = chosenSwordUpgrade.swordData.Icon;
                    upgradeOption.upgradeDescriptionDisplay.text = chosenSwordUpgrade.swordData.Description;
                    upgradeOption.upgradeNameDisplay.text = chosenSwordUpgrade.swordData.Name;
                }
            }
        }
    }

    void RemoveUpgradeOptions()
    {
        foreach (var upgradeOption in upgradeUIOptions)
        {
            upgradeOption.upgradeButton.onClick.RemoveAllListeners();
            DisableUpgradeUI(upgradeOption);
        }
    }
    public void RemoveAndApplyUpgrades()
    {
        RemoveUpgradeOptions();
        ApplyUpgradeOptions();
    }
    void DisableUpgradeUI(UpgradeUI UI)
    {
        UI.upgradeNameDisplay.transform.parent.gameObject.SetActive(false);
    }
    void EnableUpgradeUI(UpgradeUI UI)
    {
        UI.upgradeNameDisplay.transform.parent.gameObject.SetActive(true);
    }
}
