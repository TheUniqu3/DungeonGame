using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Player Inventory")]
    [SerializeField] public Weapon[] weapons;

    private WeaponShooting shoot;
    private PlayerHUD hud;

    private void Start()
    {
        InitVariables();
        GetReferences();
    }
    public Weapon GetItem(int index)
    {
        return weapons[index];
    }
    public int GetWeaponIndex(Weapon newItem)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] == newItem)
            {
                return i;
            }
        }
        return -1;
    }
    public void AddItem(Weapon newItem)
    {
        for(int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] == null)
            {
                weapons[i] = newItem;
                shoot.InitAmmo(i, newItem);
                hud.AddWeaponUI(newItem, i);
                return;
            }
        }
       
    }
    private void InitVariables()
    {
        weapons = new Weapon[5];
    }
    private void GetReferences()
    {
        shoot = GetComponent<WeaponShooting>();
        hud = GetComponent<PlayerHUD>();
    }
}
