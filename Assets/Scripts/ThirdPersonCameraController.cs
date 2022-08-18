using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Disable the third person camera
    public void DisableThirdPersonCamera()
    {
        gameObject.SetActive(false);
    }

    // Enable the third person camera
    public void EnableThirdPersonCamera()
    {
        gameObject.SetActive(true);
    }
}
