using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Consumable", menuName = "Items/Consumable")]
public class Consumable : Item
{
    public ConsumableType type;
    public AudioClip sound;

}
public enum ConsumableType { Medkit,Ammo}
