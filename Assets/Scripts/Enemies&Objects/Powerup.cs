using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private int lifeRecover;

    private HUD hud;
    private SpawnManager spawnManager;

    private void Start()
    {
        hud = FindObjectOfType<HUD>();
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

            if (playerController != null)
            {
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
}
