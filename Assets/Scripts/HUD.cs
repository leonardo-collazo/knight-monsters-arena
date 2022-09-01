using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private ProgressBar playerHealthBar;
    [SerializeField] private Text textScore;

    private GameManager gameManager;
    private PlayerController playerController;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerController = FindObjectOfType<PlayerController>();
    }

    // Updates the player health bar value
    public void UpdatePlayerHealthBarValue(float value)
    {
        playerHealthBar.BarValue = value;
    }

    // Updates the text score
    public void UpdateTextScore()
    {
        textScore.text = "Score: " + gameManager.GameScore;
    }

    // Shows HUD panel
    public void ShowHUDPanel()
    {
        gameObject.SetActive(true);
    }

    // Hides HUD panel
    public void HideHUDPanel()
    {
        gameObject.SetActive(false);
    }

    // Reset the component values of the HUD
    public void ResetComponentValues()
    {
        UpdatePlayerHealthBarValue(playerController.GetLifeInPercent());
        UpdateTextScore();
    }
}
