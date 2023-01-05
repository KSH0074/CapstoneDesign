using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    public AudioClip audioScream;
    public AudioClip audioAttack;
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
            case "Scream":
                audioSource.clip = audioScream;
                break;
            case "Attack":
                audioSource.clip = audioAttack;
                break;
            case "Death":
                audioSource.clip = audioDeath;
                break;
        }

        audioSource.Play();
    }
}
