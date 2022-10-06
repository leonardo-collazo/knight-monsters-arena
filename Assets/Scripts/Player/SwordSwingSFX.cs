using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SwordSwingSFX : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private float startingDelayTime;
    [SerializeField] private AudioClip swingSound1;
    [SerializeField] private AudioClip swingSound2;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = swingSound1;
    }

    // Plays the sounds of the sword by alternating two different types of sounds
    // with some delay depending on the time needed to start playing the sound
    public void PlaySoundSwing()
    {
        AlternateSounds();
        PlayWithDelay();
    }

    // Alternates the sounds of the sword
    void AlternateSounds()
    {
        if (audioSource.clip.GetInstanceID() == swingSound1.GetInstanceID())
        {
            audioSource.clip = swingSound2;
        }
        else
        {
            audioSource.clip = swingSound1;
        }
    }

    // Waits a certain time to start playing the sound
    void PlayWithDelay()
    {
        audioSource.PlayDelayed(startingDelayTime);
    }
}
