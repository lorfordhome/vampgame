using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<ProjectileManager> weaponSlots = new List<ProjectileManager>(2);
    public int[] weaponLevels = new int[3];
    public List<Image> weaponUIslots=new List<Image>(2);

    public void AddWeapon(int slotindex, ProjectileManager weapon)
    {
        weaponSlots[slotindex] = weapon;
        weaponLevels[slotindex] = weapon.weaponData.Level;
        weaponUIslots[slotindex].sprite = weapon.weaponData.Icon;
    }

    public void LevelUpWeapon(int slotindex)
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
        }
    }
}
