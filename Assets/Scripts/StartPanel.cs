using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
    }

    // Shows start panel
    public void ShowStartPanel()
    {
        gameObject.SetActive(true);
    }

    // Hides start panel
    public void HideStartPanel()
    {
        gameObject.SetActive(false);
    }
}
