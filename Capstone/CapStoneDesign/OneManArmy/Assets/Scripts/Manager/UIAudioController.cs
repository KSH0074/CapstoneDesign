using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioController : MonoBehaviour
{
    public AudioClip audioStatBtn; // Ω∫≈» ∫–πË«œ¥¬ button
    public AudioClip audioStatDecideBtn; // Ω∫≈» Undo Accept button
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
            case "Stat":
                audioSource.clip = audioStatBtn;
                break;
            case "StatDecide":
                audioSource.clip = audioStatDecideBtn;
                break;

        }

        audioSource.Play();
    }
}
