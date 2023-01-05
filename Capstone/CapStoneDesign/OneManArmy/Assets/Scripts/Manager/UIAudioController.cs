using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioController : MonoBehaviour
{
    public AudioClip audioStatBtn; // ���� �й��ϴ� button
    public AudioClip audioStatDecideBtn; // ���� Undo Accept button
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
