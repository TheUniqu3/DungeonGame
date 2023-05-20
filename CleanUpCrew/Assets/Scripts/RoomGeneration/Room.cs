using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    [Header("Doors")]
    [SerializeField] public GameObject DoorU;
    [SerializeField] public GameObject DoorR;
    [SerializeField] public GameObject DoorD;
    [SerializeField] public GameObject DoorL;

    [Header("Door blocks")]
    [SerializeField] public GameObject BlockU;
    [SerializeField] public GameObject BlockR;
    [SerializeField] public GameObject BlockD;
    [SerializeField] public GameObject BlockL;

    [Header("Props")]
    [SerializeField] private GameObject[] randomObjects;

    [Header("Items")]
    [SerializeField] private GameObject[] items;
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private GameObject itemspawner;
    private void Start()
    {
        Randomobjects();
        SpawnItems();
    }
    private void Randomobjects()
    {
        foreach (GameObject prop in randomObjects)
        {
            if(Random.Range(0, 2) == 1)
                prop.SetActive(true);
            else
                prop.SetActive(false);
        }    
    }
    private void SpawnItems()
    {
        if (Random.Range(0, 11) >= 6)
            if (Random.Range(0, 10) >= 5)
                Instantiate(weapons[Random.Range(0, items.Length)], itemspawner.transform.position, Quaternion.identity);
            else
                Instantiate(items[Random.Range(0, items.Length)], itemspawner.transform.position, Quaternion.identity);
    }


    public void RotateRandomly()
    {
        int count = Random.Range(0, 4);

        for (int i = 0; i < count; i++)
        {
            transform.Rotate(0, 90, 0);

            GameObject tmp = DoorL;
            DoorL = DoorD;
            DoorD = DoorR;
            DoorR = DoorU;
            DoorU = tmp;
        }
    }

}
