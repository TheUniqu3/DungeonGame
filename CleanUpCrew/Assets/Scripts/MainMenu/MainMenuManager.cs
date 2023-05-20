using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject highScoreMenu;
    [SerializeField] private ScoreSave scoreSave;
    void Start()
    {
        GetRefrences();
        ActivateMainMenu();
        ActivateCursor();
        scoreSave.InitialiseScoreFile();
        scoreSave.GetScoreFromFile();
    }
    public void ActivateCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ActivateMainMenu()
    {
        mainMenu.SetActive(true);
        highScoreMenu.SetActive(false);
    }
    public void ActivateScoreMenu(bool state)
    {
        mainMenu.SetActive(!state);
        highScoreMenu.SetActive(state);
    }
    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Quit()
    {
        Application.Quit();
    }

    private void GetRefrences()
    {
        scoreSave = GetComponent<ScoreSave>();
    }
}
