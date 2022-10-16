using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource LandingSource;
    public AudioSource musicSource;
    public AudioSource buttonSource;
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SetLandingVolume(float volume)
    {
        LandingSource.volume = volume;
    }
    public void SetButtonVolume(float volume)
    {
        buttonSource.volume = volume;
    }
    public void StopMusicSound()
    {
        musicSource.Stop();
    }
    public void StartLandingSound()
    {
        LandingSource.Play();
    }
    public void StartMusicSound()
    {
        musicSource.Play();
    }
    public void OnButtonSound()
    {
        buttonSource.Play();
    }
}
