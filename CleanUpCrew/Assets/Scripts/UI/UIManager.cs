using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private CameraController playerCam;
    [Header("Player Huds")]
    [SerializeField] private GameObject hudCanvas = null;
    [SerializeField] private GameObject pauseCanvas = null;
    [SerializeField] private GameObject endCanvas = null;

    [Header("Option Screens")]
    [SerializeField] private GameObject mainScreen = null;
    [SerializeField] private GameObject optionsScreen = null;

    [Header("Game State")]
    [SerializeField] private bool isPaused = false;

    private void Start()
    {
        GetReference();
        SetActiveHud(true);
    }
    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape) && !isPaused) 
        {
            SetActivePause(true);
        }
    }
    public void SetActiveHud(bool state)
    {

        hudCanvas.SetActive(state);
        endCanvas.SetActive(!state);
        PauseGame(!state);
        playerCam.PauseInput(!state);
    }
    public void SetActiveOptions()
    {
        optionsScreen.SetActive(true);
        mainScreen.SetActive(false);
    }
    public void SetActivePause(bool state)
    {
        pauseCanvas.SetActive(state);
        PauseGame(state);
        playerCam.PauseInput(state);
    }
    public void PauseGame(bool state)
    {
        if (state)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            isPaused = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            isPaused = false;
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
        PauseGame(false);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void GetReference()
    {
        playerCam = GetComponentInChildren<CameraController>();
    }
}
