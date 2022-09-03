using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    // Shows pause panel
    public void ShowPausePanel()
    {
        gameObject.SetActive(true);
    }

    // Hides pause panel
    public void HidePausePanel()
    {
        gameObject.SetActive(false);
    }
}
