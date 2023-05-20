using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Transform arms;
    [SerializeField] private Transform body;

    private float xRot;

    [SerializeField] private bool isPaused = false;

    void Start()
    {
        LockCursor();
    }
    void Update()
    {
        if (!isPaused)
        {
            HandleMouseLook();
        }
    }
    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90, 90);

        arms.localRotation = Quaternion.Euler(new Vector3(xRot, 0, 0));
        body.Rotate(new Vector3(0, mouseX, 0));
    }
    public void PauseInput(bool state)
    {
        isPaused = state;
    }
    public void SetSensitivity(float sense)
    {
        mouseSensitivity = sense;
    }
    private void LockCursor()
    {
        Cursor.lockState= CursorLockMode.Locked;
    }
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
