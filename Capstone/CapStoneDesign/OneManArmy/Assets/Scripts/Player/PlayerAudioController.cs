using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public AudioClip audioWalk;
    public AudioClip audioAttack;
    public AudioClip audioDodge;
    public AudioClip audioDeath;
    AudioSource audioSource;

    void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string action)
    {
        audioSource.Stop();

        switch (action)
        {
            case "Walk":
                audioSource.clip = audioWalk;
                break;
            case "Attack":
                audioSource.clip = audioAttack;
                break;
            case "Dodge":
                audioSource.clip = audioDodge;
                break;
            case "Death":
                audioSource.clip = audioDeath;
                break;
        }

        audioSource.Play();
    }

}
