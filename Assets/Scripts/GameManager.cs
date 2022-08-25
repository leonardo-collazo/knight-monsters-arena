using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables

    private float gameScore = 0;

    public bool IsGameActive { get; set; }
    public bool IsGamePaused { get; set; }

    [SerializeField] private float combatCooldownTime;
    [SerializeField] private float receiveDamageCooldownTime;

    [SerializeField] private ThirdPersonCameraController thirdPersonCamera;
    [SerializeField] private PausePanel pausePanel;
    [SerializeField] private HUD hud;

    private PlayerController playerController;
    private SpawnManager spawnManager;

    public float CombatCooldownTime { get => combatCooldownTime; }
    public float ReceiveDamageCooldownTime { get => receiveDamageCooldownTime; }
    public float GameScore
    {
        get { return gameScore; }

        set
        {
            gameScore = value < 0 ? throw new System.Exception("The score cannot be negative") : value;
        }
    }

    #endregion

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsGamePaused && IsGameActive)
            {
                PauseGame();
            }
            else
            {
                ReanudeGame();
            }
        }
    }

    #region Game main management methods

    // Starts the game
    public void StartGame()
    {
        playerController.Life = playerController.MaxLife;

        hud.ConfigureToStartGame();
        thirdPersonCamera.EnableThirdPersonCamera();

        IsGameActive = true;
    }

    // Restarts the game
    public void RestartGame()
    {
        playerController.GetPlayerAnimator().SetBool("Dead_b", false);
        StartGame();
    }

    // Pauses the game
    public void PauseGame()
    {
        Time.timeScale = 0;

        hud.HideHUDPanel();
        pausePanel.ShowPausePanel();

        IsGamePaused = true;
        IsGameActive = false;
    }

    // Reanudes the game
    public void ReanudeGame()
    {
        Time.timeScale = 1;

        pausePanel.HidePausePanel();
        hud.ShowHUDPanel();

        IsGamePaused = false;
        IsGameActive = true;
    }

    // Checks if it's game over and if so, the game ends
    public void GameOver()
    {
        IsGameActive = false;
        playerController.GetPlayerAnimator().SetBool("Dead_b", true);
        spawnManager.CancelInvoke();
    }

    // Exites the game
    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion

    // Makes the player immune to hits
    public IEnumerator ImmunizePlayer(float time)
    {
        playerController.IsImmune = true;
        yield return new WaitForSeconds(time);
        playerController.IsImmune = false;
    }

    // Updates the game score
    public void UpdateScore(float value)
    {
        GameScore += value;
    }
}
