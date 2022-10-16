using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    
    public AudioSource audioSource;
    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioRun;
    public AudioClip audioDamaged;
    public AudioClip audioDie;
    public AudioClip audioHeal;
    public AudioClip audioDodge;
    public AudioClip audioLanding;
   
    public void SetIngameVolume(float volume)
    {
        audioSource.volume=volume;
    }

    
    public void PlaySound(string action)
    {
        
        switch (action)
        {
            case "JUMP":
                audioSource.clip = audioJump;
                break;
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                break;
            case "ATTACK":
                audioSource.clip = audioAttack;
                break;
            case "DIE":
                audioSource.clip = audioDie;
                break;
            case "HEAL":
                audioSource.clip = audioHeal;
                break;
            case "DODGE":
                audioSource.clip = audioDodge;
                break;
            case "LANDING":
                audioSource.clip = audioLanding;
                break;
            case "RUN":
                audioSource.clip = audioRun;
                break;
        }

        audioSource.PlayOneShot(audioSource.clip);
    }
 
}
