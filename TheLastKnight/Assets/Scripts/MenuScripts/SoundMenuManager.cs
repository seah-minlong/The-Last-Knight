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
    [Tooltip("AudioClip should be preloaded")]
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
        musicSource.loop = true; 
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
        musicSource.loop = true; 
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

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PauseButtonSound()
    {
        SFXSource.PlayOneShot(openPauseMenu);
    }

    public void FadeOutAndChangeMusic(AudioClip clip)
    {
        StartCoroutine(FadeOutAndChangeMusicCoroutine(clip, 1.0f));
    }
    
    public void FadeOutAndPauseMusic() 
    {
        StartCoroutine(FadeOutAndPauseMusicCoroutine(1.0f));
    }

    private IEnumerator FadeOutAndPauseMusicCoroutine(float fadeDuration)
    {
        float currentTime = 0;
        float startVolume = musicSource.volume;

        // Fade out
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0, currentTime / fadeDuration);
            yield return null;
        }

        musicSource.Pause(); // Use Pause instead of Stop to keep the current clip
        musicSource.volume = startVolume; // Reset volume to the start volume
    }

    private IEnumerator FadeOutAndChangeMusicCoroutine(AudioClip newClip, float fadeDuration)
    {
        float currentTime = 0;
        float startVolume = musicSource.volume;

        // Fade out
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0, currentTime / fadeDuration);
            yield return null;
        }

        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.Play();
        currentTime = 0;

        // Fade in
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0, startVolume, currentTime / fadeDuration);
            yield return null;
        }

        musicSource.volume = startVolume;
    }

}
