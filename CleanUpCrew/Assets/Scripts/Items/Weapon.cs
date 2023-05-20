using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon", menuName ="Items/weapon")]
public class Weapon : Item
{
    public GameObject prefab;
    public GameObject muzzleFlashParticles;
    public Sprite crosshair;
    public AudioClip sound;
    public int magazineSize;
    public int storedAmmo;
    public float range;
    public float fireRate;
    public int damage;
    public WeaponType weaponType;
    public WeaponStyle weaponStyle;
}

public enum WeaponType {neonBlater,shotblaster,Mtar,lasergun,stormgun}
public enum WeaponStyle {pistol, Rifle}