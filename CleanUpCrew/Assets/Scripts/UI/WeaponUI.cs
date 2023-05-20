using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class WeaponUI : MonoBehaviour
{

    [SerializeField] private Image[] icon;
    [SerializeField] private Image crosshair;
    [SerializeField] private TextMeshProUGUI magazineSizeText;
    [SerializeField] private TextMeshProUGUI storedAmmoText;

    public void UpdateInfo(int inventoryNumber, Sprite weaponCross, int magazineSize, int storedAmmo)
    {
        ResetWeaponIcons();
        icon[inventoryNumber].rectTransform.sizeDelta = new Vector2(160, 160);
        crosshair.sprite = weaponCross;
        magazineSizeText.text = magazineSize.ToString();
        storedAmmoText.text= storedAmmo.ToString();
    }
    public void AddWeapon(Sprite WeaponIcon,int inventoryNumber)
    {
        icon[inventoryNumber].sprite = WeaponIcon;
    }
    public void ResetWeaponIcons()
    {
        for(int i = 0; i < icon.Length; i++) 
        {
            icon[i].rectTransform.sizeDelta = new Vector2(80, 80);
        }
    }

}
