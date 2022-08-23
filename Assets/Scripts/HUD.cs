using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private ProgressBar playerHealthBar;

    public void UpdatePlayerHealthBarValue(float value)
    {
        playerHealthBar.BarValue = value;
    }

    public void ShowHUDPanel()
    {
        gameObject.SetActive(true);
    }

    public void HideHUDPanel()
    {
        gameObject.SetActive(false);
    }
}
