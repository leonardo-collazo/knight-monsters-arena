using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float CombatCooldownTime { get; } = 2.5f;

    public bool isGameActive;

    private PlayerController playerController;
    private SpawnManager spawnManager;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Starts the game
    public void StartGame()
    {
        playerController.life = playerController.maxLife;
        isGameActive = true;
        spawnManager.StartAllSpawns();
    }

    // Restarts the game
    public void RestartGame()
    {
        playerController.GetPlayerAnimator().SetBool("Dead_b", false);
        playerController.life = playerController.maxLife;
        isGameActive = true;
        spawnManager.StartAllSpawns();
    }

    // Checks if it's game over and if so, the game ends
    public void GameOver()
    {
        if (playerController.life == 0)
        {
            isGameActive = false;
            playerController.GetPlayerAnimator().SetBool("Dead_b", true);
            spawnManager.CancelInvoke();
        }
    }

    // Makes the player immune to hits
    public IEnumerator ImmunizePlayer(float time)
    {
        playerController.IsImmune = true;
        yield return new WaitForSeconds(time);
        playerController.IsImmune = false;
    }
}
