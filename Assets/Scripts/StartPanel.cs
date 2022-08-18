using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : MonoBehaviour
{
    public void ShowStartPanel()
    {
        gameObject.SetActive(true);
    }

    public void HideStartPanel()
    {
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
