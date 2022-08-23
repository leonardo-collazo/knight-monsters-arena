using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float combatCooldownTime;
    [SerializeField] private float receiveDamageCooldownTime;
    
    public float CombatCooldownTime { get => combatCooldownTime; }
    public float ReceiveDamageCooldownTime { get => receiveDamageCooldownTime; }

    public bool isGameActive;

    [SerializeField] private ThirdPersonCameraController thirdPersonCamera;
    [SerializeField] private HUD hud;

    private PlayerController playerController;
    private SpawnManager spawnManager;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    // Starts the game
    public void StartGame()
    {
        playerController.Life = playerController.MaxLife;

        hud.ShowHUDPanel();
        hud.UpdatePlayerHealthBarValue(playerController.Life);

        isGameActive = true;
        thirdPersonCamera.EnableThirdPersonCamera();
    }

    // Restarts the game
    public void RestartGame()
    {
        playerController.GetPlayerAnimator().SetBool("Dead_b", false);
        StartGame();
    }

    // Checks if it's game over and if so, the game ends
    public void GameOver()
    {
        isGameActive = false;
        playerController.GetPlayerAnimator().SetBool("Dead_b", true);
        spawnManager.CancelInvoke();
    }

    // Makes the player immune to hits
    public IEnumerator ImmunizePlayer(float time)
    {
        playerController.IsImmune = true;
        yield return new WaitForSeconds(time);
        playerController.IsImmune = false;
    }
}
