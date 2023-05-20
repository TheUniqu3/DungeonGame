using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
//using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class EquipmentManager : MonoBehaviour
{
    [Header("Gun Arm")]
    [SerializeField] private Transform weaponHolder;

    private WeaponShooting shooting;
    private Animator anim;
    private Inventory inventory;
    private PlayerHUD hud;


    [Header("Current Gun")]
    public int currentlyEquiped = 0;
    private GameObject currentWeaponObject = null;
    public Transform currentWeaponBarrel = null;

    [Header("Default gun")]
    [SerializeField] public Weapon defaultweapon;

    private void Start()
    {
        GetReferences();
        StartCoroutine(StartWeapon(defaultweapon));

    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentlyEquiped != 0 && inventory.GetItem(0) != null)
        {
            UnequipWeapon();
            EquipWeapon(inventory.GetItem(0),0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && currentlyEquiped != 1 && inventory.GetItem(1) != null)
        {
            UnequipWeapon();
            EquipWeapon(inventory.GetItem(1),1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && currentlyEquiped != 2 && inventory.GetItem(2) != null)
        {
            UnequipWeapon();
            EquipWeapon(inventory.GetItem(2),2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && currentlyEquiped != 3 && inventory.GetItem(3) != null)
        {
            UnequipWeapon();
            EquipWeapon(inventory.GetItem(3),3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && currentlyEquiped != 4 && inventory.GetItem(4) != null)
        {
            UnequipWeapon();
            EquipWeapon(inventory.GetItem(4),4);
        }
    }

    //for other animations
    private void EquipWeapon(Weapon weapon , int selectedWeapon)
    {
        currentlyEquiped = selectedWeapon;
        anim.SetInteger("weaponType",(int)weapon.weaponStyle);
        currentWeaponObject = Instantiate(weapon.prefab, weaponHolder);
        currentWeaponBarrel = currentWeaponObject.transform.GetChild(0);
        shooting.SetItemIndex(currentlyEquiped);
        hud.UpdateWeaponUI(selectedWeapon, weapon.crosshair, shooting.GetAmmoMagazine(currentlyEquiped), shooting.GetAmmoStored(currentlyEquiped));
    }
    private void UnequipWeapon()
    {
        anim.SetTrigger("UnequipWeapon");
        Destroy(currentWeaponObject);
    }
    private IEnumerator StartWeapon(Weapon defaultweapon)
    {
        yield return new WaitForSeconds(1);
        inventory.AddItem(defaultweapon);
        EquipWeapon(inventory.GetItem(0), 0);
    }

    private void GetReferences()
    {
        anim = GetComponentInChildren<Animator>();
        inventory= GetComponentInChildren<Inventory>();
        hud = GetComponent<PlayerHUD>();
        shooting = GetComponent<WeaponShooting>();
    }
}
