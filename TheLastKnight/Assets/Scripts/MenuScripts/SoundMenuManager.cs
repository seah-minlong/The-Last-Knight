using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundMenuManager : MonoBehaviour
{
    public static SoundMenuManager instance;

    [Header("------------Audio Source-----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;


    [Header("--------------Audio Clip-------------")]
    [SerializeField] AudioClip background;
    [SerializeField] AudioClip openPauseMenu;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    // for menu sfx
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void ChangeBackgroundMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }


    public void ResumeMusic()
    {
        musicSource.UnPause();
    }

    public void PauseSound()
    {
        SFXSource.PlayOneShot(openPauseMenu);
    }
}
