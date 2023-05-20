using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private ProgressBar healthBar;
    [SerializeField] private WeaponUI weaponUI;
    [SerializeField] private Score scoreUI;
    [SerializeField] private GameObject Tip;
    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        healthBar.SetValues(currentHealth, maxHealth);
    }
    public void UpdateWeaponUI(int slotNumber, Sprite weaponCross, int magAmmo, int storedAmmo)
    {
        weaponUI.UpdateInfo(slotNumber, weaponCross, magAmmo, storedAmmo);
    }
    public void AddWeaponUI(Weapon newWeapon, int slotNumber)
    {
        weaponUI.AddWeapon(newWeapon.icon, slotNumber);
    }
    public void UpdatescoreUI(int score)
    {
       scoreUI.AddToScore(score);
    }
    public void ShowTipUI(bool state)
    {
        Tip.SetActive(state);
    }
    public void UpdateScoreEnemyKilled()
    {
        scoreUI.Enemykilled();
    }
}
