using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables

    private float gameScore;

    public bool IsGameActive { get; set; }
    public bool IsGamePaused { get; set; }

    [SerializeField] private float combatCooldownTime;
    [SerializeField] private float receiveDamageCooldownTime;

    [SerializeField] private ThirdPersonCameraController thirdPersonCamera;
    [SerializeField] private GameObject pausePanelGroup;
    [SerializeField] private GameOverMenu gameOverMenu;
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
        if (Input.GetKeyDown(KeyCode.Escape) && IsGameActive)
        {
            if (!IsGamePaused)
            {
                PauseGame();
            }
            else
            {
                ReanudeGame();
            }
        }
    }

    #region Game management methods

    // Starts the game
    public void StartGame()
    {
        playerController.Life = playerController.MaxLife;

        hud.ShowHUDPanel();
        hud.ResetComponentValues();
        thirdPersonCamera.EnableThirdPersonCamera();

        IsGameActive = true;
    }

    // Restarts the game
    public void PlayAgain()
    {
        gameOverMenu.HideGameOverMenu();
        playerController.RecoverFromDeath();
        ImmunizePlayer(playerController.TimeRecoveringFromDeath);
        
        StartGame();
        spawnManager.StartAllSpawns();
    }

    // Pauses the game
    public void PauseGame()
    {
        Time.timeScale = 0;

        hud.HideHUDPanel();
        pausePanel.ShowPausePanel();

        IsGamePaused = true;
    }

    // Reanudes the game
    public void ReanudeGame()
    {
        Time.timeScale = 1;

        DisablePausePanelGroup();
        hud.ShowHUDPanel();

        IsGamePaused = false;
    }

    // Checks if it's game over and if so, the game ends
    public void GameOver()
    {
        IsGameActive = false;
        thirdPersonCamera.DisableThirdPersonCamera();

        spawnManager.CancelInvoke();
        hud.HideHUDPanel();
        gameOverMenu.ShowGameOverMenu();
    }

    // Exites the game
    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion

    #region Other methods

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

    // Reload the actual scene
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, new LoadSceneParameters());
    }

    // Disable all of the panels that could be open when the game is paused
    private void DisablePausePanelGroup()
    {
        for (int i = 0; i < pausePanelGroup.transform.childCount; i++)
        {
            pausePanelGroup.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    #endregion
}
