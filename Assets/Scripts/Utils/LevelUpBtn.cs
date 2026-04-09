using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpBtn : MonoBehaviour
{
    public TMP_Text weaponName;
    public TMP_Text weaponDesc;
    public Image WeaponIcon;

    private Weapon assignedWeapon;
    public void activatebutton(Weapon weapon)
    {
        if (weapon.stats[weapon.weaponLevel].active)
        {
            weaponName.text = weapon.name;
            weaponDesc.text = weapon.stats[weapon.weaponLevel].desc;
            WeaponIcon.sprite = weapon.weaponIcon;
            assignedWeapon = weapon;
        }
    }

    public void selectUpgrade()
    {    
        if (assignedWeapon != null)
        {
            assignedWeapon.LevelUp();
            UIController.Instance.LeveUpMenuC();
        }
    }
}
