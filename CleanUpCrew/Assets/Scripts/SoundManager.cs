using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;
    [Header("Sound Sources")]
    [SerializeField] private AudioSource SoundEffects;
    [SerializeField] private AudioSource backgroundMusic;

    [Header("Sound Sources")]
    [SerializeField] public float  FxVolume = 1f;

    void Start()
    {
        backgroundMusic.Play();
    }
    public void PlaysoundEffect(AudioClip clip)
    {
        SoundEffects.PlayOneShot(clip, FxVolume);
    }

    public void SetVolumeSoundFx(float effect)
    {
        FxVolume = effect;
    }
    public void SetVolumeSoundMusic(float effect)
    {
        mixer.SetFloat("BackVol", (-80+ effect*100));
    }
}
