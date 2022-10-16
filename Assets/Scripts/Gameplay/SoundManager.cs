using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour, IHasNoise
{
    private AudioSource soundPlayer;

    public AudioSource SoundPlayer { get => soundPlayer; }

    void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
    }

    public void MakeNoise()
    {
        SoundPlayer.Play();
    }

    public void StopNoise()
    {
        SoundPlayer.Stop();
    }
}
