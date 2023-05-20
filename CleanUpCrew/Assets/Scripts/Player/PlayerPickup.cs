using System.Collections;
//using System.Collections.Generic;
//using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [Header("Player PickUp")]
    [SerializeField] private float pickupRange;
    [SerializeField] private LayerMask pickUpLayer;

    private Camera cam;
    private Inventory inventory;
    private PlayerStats stats;
   

    private WeaponShooting shoot;

    private void Start()
    {
        GetReferences();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit, pickupRange, pickUpLayer))
            {
                Debug.Log("Hit:" + hit.transform.name);
                if (hit.transform.GetComponent<ItemObject>().item as Weapon)
                {
                    Weapon newItem = hit.transform.GetComponent<ItemObject>().item as Weapon;
                    inventory.AddItem(newItem);
                } else
                {
                    Consumable newItem = hit.transform.GetComponent<ItemObject>().item as Consumable;
                    if(newItem.type == ConsumableType.Medkit) 
                    {
                        Debug.Log("Heal");
                        stats.Heal(stats.GetMaxHealth());
                    }
                    else
                    {
                        Debug.Log("Ammo");
                        shoot.AddAmmo(40);
                    }
                }
                Destroy(hit.transform.gameObject);
            }
        }
    }

    private void GetReferences()
    {
        cam = GetComponentInChildren<Camera>();
        inventory= GetComponent<Inventory>();
        shoot = GetComponent<WeaponShooting>();
        stats = GetComponent<PlayerStats>();
    }
}
