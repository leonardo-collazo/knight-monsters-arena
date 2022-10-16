using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour, IHasNoise
{
    [SerializeField] private int lifeRecover;
    [SerializeField] private AudioClip healthSound;

    private HUD hud;
    private SpawnManager spawnManager;
    private SoundManager soundManager;

    private void Start()
    {
        hud = FindObjectOfType<HUD>();
        spawnManager = FindObjectOfType<SpawnManager>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

            if (playerController != null)
            {
                MakeNoise();
                HealPlayer(playerController);
            }

            spawnManager.SpawnPowerupVFX(transform);
            Destroy(gameObject);
        }
    }

    // Heals the player
    void HealPlayer(PlayerController playerController)
    {
        playerController.Life += lifeRecover;
        hud.UpdatePlayerHealthBarValue(playerController.GetLifeInPercent());
    }

    // Plays health noise
    public void MakeNoise()
    {
        soundManager.SoundPlayer.clip = healthSound;
        soundManager.MakeNoise();
    }

    // Stops health noise
    public void StopNoise()
    {
        soundManager.StopNoise();
    }
}
