using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPanel : MonoBehaviour
{
    // Shows credits panel
    public void ShowCreditsPanel()
    {
        gameObject.SetActive(true);
    }

    // Hides credits panel
    public void HideCreditsPanel()
    {
        gameObject.SetActive(false);
    }
}
