using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class WeaponShooting : MonoBehaviour
{
    private float lastShootTime = 0;

    [SerializeField] private int currentEquipedWeapon = 0;

    [SerializeField] private int [,] gunAmmo = new int [2,5];

    [SerializeField] private AudioClip reload;

    private Camera cam;
    private Inventory inventory;
    private EquipmentManager manager;
    private PlayerHUD hud;
    private Score score;
    private SoundManager sound;

    [SerializeField] private bool canShoot = true;

    [SerializeField] private GameObject hitParticles = null;

    [SerializeField] private TrailRenderer bulletTrail = null;


    private void Start()
    {
        GetReferences();
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            Shoot(currentEquipedWeapon);

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload(currentEquipedWeapon);
            sound.PlaysoundEffect(reload);
        }
    }

    private void RaycastShoot(Weapon currentWeapon)
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2,Screen.height / 2));
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit,currentWeapon.range))
        {
            TrailRenderer trail = Instantiate(bulletTrail, manager.currentWeaponBarrel.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit));

            if(hit.collider.CompareTag("Enemy"))
            {
                CharacterStats enemy = hit.transform.GetComponent<CharacterStats>();
                enemy.TakeDamage(currentWeapon.damage);
                score.AddToDamage(currentWeapon.damage);
                SpawnHitParticles(hit.point, hit.normal);
                hud.UpdatescoreUI(10);
            }
        }
        Instantiate(currentWeapon.muzzleFlashParticles, manager.currentWeaponBarrel);
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;
        while(time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime/Trail.time;
            yield return null;
        }
        Destroy(Trail.gameObject,Trail.time);

    }
    private void Shoot(int current)
    {
        IsWeaponEmpty(current);
        if (canShoot)
        {
            Weapon currentWeapon = inventory.GetItem(manager.currentlyEquiped);

            if (Time.time > lastShootTime + currentWeapon.fireRate)
            {
                lastShootTime = Time.time;
                RaycastShoot(currentWeapon);
                UseAmmo(current, 1);
                hud.UpdateWeaponUI(current, currentWeapon.crosshair, gunAmmo[0, current], gunAmmo[1, current]);
                sound.PlaysoundEffect(currentWeapon.sound);
            }
        }
        else
        {
            hud.ShowTipUI(true);
        }
    }

    private void Reload(int current)
    {
        if(gunAmmo[0, current] != (inventory.GetItem(current).magazineSize))
        {
            int ammoToReload = (inventory.GetItem(current).magazineSize - gunAmmo[0, current]);
            if (gunAmmo[1, current] >= ammoToReload)
            {
                gunAmmo[1, current] -= (inventory.GetItem(current).magazineSize - gunAmmo[0, current]);
                gunAmmo[0, current] = (inventory.GetItem(current).magazineSize);
            }
            else if(gunAmmo[1, current] < ammoToReload && (gunAmmo[0, current] + ammoToReload) <= inventory.GetItem(current).magazineSize)
            {
                gunAmmo[0, current] += gunAmmo[1, current];
                gunAmmo[1, current] = 0;
            }
            Weapon currentWeapon = inventory.GetItem(manager.currentlyEquiped);

            hud.UpdateWeaponUI(current, currentWeapon.crosshair, gunAmmo[0, current], gunAmmo[1, current]);
            hud.ShowTipUI(false);
        }
    }
    private void IsWeaponEmpty(int current)
    {
        if (gunAmmo[0, current] <= 0)
        {
            canShoot = false;
            hud.ShowTipUI(true);
            
        }
        else 
        {
            canShoot = true;
            hud.ShowTipUI(false);
        }

    }

    private void SpawnHitParticles(Vector3 position, Vector3 noraml)
    {
        Instantiate(hitParticles, position, Quaternion.FromToRotation(Vector3.up,noraml));
    }
    private void UseAmmo(int current,int currentAmmoUsed)
    {
        if (gunAmmo[0,current] > 0)
        {
            gunAmmo[0, current] -= currentAmmoUsed;
        }
        
    }
    public void AddAmmo(int ammoAdded)
    {
        for(int i = 0;i<5;i++)
        {
            if (gunAmmo[1, i] != null)
                gunAmmo[1, i] += ammoAdded;
        }
        Weapon currentWeapon = inventory.GetItem(manager.currentlyEquiped);
        hud.UpdateWeaponUI(currentEquipedWeapon, currentWeapon.crosshair, gunAmmo[0, currentEquipedWeapon], gunAmmo[1, currentEquipedWeapon]);
    }
    public void InitAmmo(int Item,Weapon weapon)
    {
        gunAmmo[0, Item] = weapon.magazineSize;
        gunAmmo[1, Item] = weapon.storedAmmo;
    }
    public int GetAmmoMagazine(int Item)
    {
         return gunAmmo[0, Item];
       
    }
    public int GetAmmoStored(int Item)
    {
        return gunAmmo[1, Item] ;
    }
    public void SetItemIndex(int Item)
    {
        currentEquipedWeapon = Item;
    }

    private void GetReferences()
    {
        cam = GetComponentInChildren<Camera>();
        inventory= GetComponent<Inventory>();
        manager= GetComponent<EquipmentManager>();
        hud = GetComponent<PlayerHUD>();
        score = GetComponentInChildren<Score>();
        sound = GetComponent<SoundManager>();
    }
}
