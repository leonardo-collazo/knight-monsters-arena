using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private float turningVolumeDownSpeed;

    [SerializeField] private AudioClip initialMusic;
    [SerializeField] private AudioClip combatMusic;

    private AudioSource music;

    private void Start()
    {
        music = GetComponent<AudioSource>();
    }

    private void PlayMusic()
    {
        music.Play();
    }

    private void StopMusic()
    {
        music.Stop();
    }

    private void SetMaxVolume()
    {
        music.volume = 1;
    }

    private IEnumerator TurnVolumeDownSlowly(float volume)
    {
        while (music.volume > volume)
        {
            music.volume -= turningVolumeDownSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private void LoadCombatMusic()
    {
        music.clip = combatMusic;
    }

    private IEnumerator TurnVolumeDownAndStop(float volume)
    {
        StartCoroutine(TurnVolumeDownSlowly(volume));
        
        while (music.volume > volume)
        {
            yield return new WaitForEndOfFrame();
        }

        StopMusic();
    }

    public void StartCoroutineTurnVolumeDownAndStop(float volume)
    {
        StartCoroutine(TurnVolumeDownAndStop(volume));
    }

    public void PrepareBattleEnvironment()
    {
        LoadCombatMusic();
        SetMaxVolume();
        PlayMusic();
    }
}
