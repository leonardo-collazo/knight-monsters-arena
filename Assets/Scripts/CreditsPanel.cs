using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPanel : MonoBehaviour
{
    public void ShowCreditsPanel()
    {
        gameObject.SetActive(true);
    }

    public void HideCreditsPanel()
    {
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
