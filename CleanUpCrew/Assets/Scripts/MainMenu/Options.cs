using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private CameraController playerCamera;
    [SerializeField] private SoundManager soundManager;

    [SerializeField] private Slider sensetivitybar;
    [SerializeField] private TextMeshProUGUI sensetivityText;
    [SerializeField] private Slider effectsBar;
    [SerializeField] private TextMeshProUGUI effectsText;

    [SerializeField] private Slider musicBar;
    [SerializeField] private TextMeshProUGUI musicText;
    
    [Header("Settings")]
    [SerializeField] public float musicValue = 1;
    [SerializeField] public float effectsValue = 1;
    [SerializeField] public float sensetivityValue = 1;
    void Awake()
    {
        PlayerPrefs.SetFloat("playerSensetivity", 1f);
        PlayerPrefs.SetFloat("playerFxVolume", 0.5f);
        PlayerPrefs.SetFloat("playerMusicVolume", 0.5f);
    }
    void Start()
    {
        GetReferences();
        playerCamera.SetSensitivity(sensetivityValue * 100.0f);
        soundManager.SetVolumeSoundFx(effectsValue);
        soundManager.SetVolumeSoundMusic(musicValue);
    }
    public void SaveSensitivity()
    {
        sensetivityText.text = (Mathf.Round(sensetivitybar.value * 100.0f) * 0.01f).ToString();
        playerCamera.SetSensitivity(sensetivitybar.value * 100.0f);
        PlayerPrefs.SetFloat("playerSensetivity", sensetivitybar.value);
    }
    public void SaveGameSoundFx()
    {
        effectsText.text = Mathf.Round(effectsBar.value * 100).ToString();
        soundManager.SetVolumeSoundFx(effectsBar.value);
        PlayerPrefs.SetFloat("playerFxVolume", effectsBar.value);
    }
    public void SaveGameSoundMusic()
    {
        musicText.text = Mathf.Round(musicBar.value * 100).ToString();
        soundManager.SetVolumeSoundMusic(musicBar.value);
        PlayerPrefs.SetFloat("playerMusicVolume", musicBar.value);
    }

    public void Exit()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    private void GetReferences()
    {
        musicValue = PlayerPrefs.GetFloat("playerSensetivity");
        effectsValue = PlayerPrefs.GetFloat("playerFxVolume");
        sensetivityValue = PlayerPrefs.GetFloat("playerMusicVolume");
    }
}
