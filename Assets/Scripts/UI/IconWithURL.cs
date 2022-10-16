using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.EventSystems;

public class IconWithURL : Icon, IURL
{
    [SerializeField] private string url;

    public void OpenURL()
    {
        Application.OpenURL(url);
    }
}
