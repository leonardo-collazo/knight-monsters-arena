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
    [SerializeField] private float raisePlayerGateWaitingTime;

    [SerializeField] private ThirdPersonCameraController thirdPersonCamera;
    [SerializeField] private GameObject pausePanelGroup;
    [SerializeField] private GameOverMenu gameOverMenu;
    [SerializeField] private StartPanel startPanel;
    [SerializeField] private PausePanel pausePanel;
    [SerializeField] private HUD hud;

    private PlayerGateController playerGateController;
    private PlayerController playerController;
    private SpawnManager spawnManager;
    private MusicManager musicManager;

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
        musicManager = FindObjectOfType<MusicManager>();
        playerGateController = FindObjectOfType<PlayerGateController>();
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
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerController.Life = playerController.MaxLife;

        startPanel.HideStartPanel();
        musicManager.AdjustVolumeAndStop(0);

        yield return new WaitForSeconds(raisePlayerGateWaitingTime);

        playerGateController.RaiseHarrows();
        thirdPersonCamera.EnableThirdPersonCamera();
        hud.ShowHUDPanel();
        hud.ResetComponentValues();
        IsGameActive = true;
    }

    // Restarts the game
    public void PlayAgain()
    {
        gameOverMenu.HideGameOverMenu();
        playerController.RecoverFromDeath();
        ImmunizePlayer(playerController.TimeRecoveringFromDeath);
        Cursor.lockState = CursorLockMode.Confined;

        playerController.Life = playerController.MaxLife;
        spawnManager.StartAllSpawns();
        IsGameActive = true;
    }

    // Pauses the game
    public void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;

        hud.HideHUDPanel();
        pausePanel.ShowPausePanel();

        IsGamePaused = true;
    }

    // Reanudes the game
    public void ReanudeGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;

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

        Cursor.lockState = CursorLockMode.Confined;
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
        hud.UpdateTextScore();
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
