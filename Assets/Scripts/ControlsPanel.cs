using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsPanel : MonoBehaviour
{
    // Shows controls panel
    public void ShowControlsPanel()
    {
        gameObject.SetActive(true);
    }

    // Hides controls panel
    public void HideControlsPanel()
    {
        gameObject.SetActive(false);
    }
}
