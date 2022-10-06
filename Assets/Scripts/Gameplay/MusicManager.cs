using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private float turningVolumeDownSpeed;

    [SerializeField] private AudioClip initialMusic;
    [SerializeField] private AudioClip combatMusic;

    private AudioSource music;

    public void Start()
    {
        music = GetComponent<AudioSource>();
    }
    
    // Plays the music
    public void PlayMusic()
    {
        music.Play();
    }

    // Stops the music
    public void StopMusic()
    {
        music.Stop();
    }
    
    // Set the volume to the maximum
    public void SetMaxVolume()
    {
        music.volume = 1;
    }

    // Loads combat music
    public void LoadCombatMusic()
    {
        music.clip = combatMusic;
    }

    // Lowers the volume slowly over a set period of time
    private IEnumerator AdjustVolume(float volume)
    {
        if (music.volume > volume)
        {
            while (music.volume > volume)
            {
                music.volume -= turningVolumeDownSpeed * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        else if (music.volume < volume)
        {
            while (music.volume < volume)
            {
                music.volume += turningVolumeDownSpeed * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }

    // Adjust the volume to certain value and stops
    private IEnumerator AdjustVolumeAndStopMusicCoroutine(float volume)
    {
        StartCoroutine(AdjustVolume(volume));

        if (music.volume > volume)
        {
            while (music.volume > volume)
            {
                yield return new WaitForEndOfFrame();
            }
        }
        else if (music.volume > volume)
        {
            while (music.volume > volume)
            {
                yield return new WaitForEndOfFrame();
            }
        }

        StopMusic();
    }

    public void AdjustVolumeAndStop(float volume)
    {
        StartCoroutine(AdjustVolumeAndStopMusicCoroutine(volume));
    }

    public void PrepareBattleEnvironment()
    {
        LoadCombatMusic();
        SetMaxVolume();
        PlayMusic();
    }
}
