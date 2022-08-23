using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupFeatures : MonoBehaviour
{
    private int lifeRecover = 20;

    private HUD hud;

    private void Start()
    {
        hud = FindObjectOfType<HUD>();
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

            Destroy(gameObject);
        }
    }

    public void HealPlayer(PlayerController playerController)
    {
        playerController.Life += lifeRecover;
        hud.UpdatePlayerHealthBarValue(playerController.Life);
    }
}
