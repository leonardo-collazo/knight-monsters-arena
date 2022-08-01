using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupFeatures : MonoBehaviour
{
    private int lifeRecover = 20;

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
        if ((playerController.life + lifeRecover) > playerController.maxLife)
        {
            playerController.life = playerController.maxLife;
        }
        else
        {
            playerController.life += lifeRecover;
        }
    }
}
