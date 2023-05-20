using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    public float speed = 5;
    public Room[] RoomPrefabs;
    public bool RoomSpawned = false;
    private float time;
    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime, Space.World);

    }

}

