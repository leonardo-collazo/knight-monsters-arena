using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnButtonAction : MonoBehaviour
{
    [SerializeField] private StartPanel startPanel;
    [SerializeField] private PausePanel pausePanel;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Return button action
    public void Return()
    {
        if (gameManager.IsGamePaused)
        {
            pausePanel.ShowPausePanel();
        }
        else
        {
            startPanel.ShowStartPanel();
        }
    }
}
