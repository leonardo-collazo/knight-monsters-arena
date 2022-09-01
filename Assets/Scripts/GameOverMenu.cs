using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    // Shows death panel
    public void ShowGameOverMenu()
    {
        gameObject.SetActive(true);
    }

    // Hides death panel
    public void HideGameOverMenu()
    {
        gameObject.SetActive(false);
    }
}
