using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [Header("Door Prefabs")]
    [SerializeField] private Door DoorL;
    [SerializeField] private Door DoorR;

    [Header("Player has been here")]
    [SerializeField] private bool playerWasHere = false;
    private Light doorLightInside = null;
    private Light doorLightOutside = null;
    [SerializeField] private GameObject lightGameObjectInside;
    [SerializeField] private GameObject lightGameObjectOutside;

    private void Start()
    {
        doorLightInside = lightGameObjectInside.GetComponentInChildren<Light>();
        doorLightOutside = lightGameObjectOutside.GetComponentInChildren<Light>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!DoorR.IsOpen && other.CompareTag("Player"))
        {
            DoorL.Open(/*other.transform.position*/);
            DoorR.Open(/*other.transform.position*/);
        }
        if (!playerWasHere && other.CompareTag("Player"))
        {
            doorLightInside.color = new Color32(33, 255, 0, 255);
            doorLightOutside.color = new Color32(33, 255, 0, 255);
            playerWasHere = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (DoorR.IsOpen)
        {
            DoorR.Close();
            DoorL.Close();
        }
        
    }
}
