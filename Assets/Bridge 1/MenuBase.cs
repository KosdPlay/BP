using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuBase : PauseGame
{
    [SerializeField] protected GameObject menu;
    protected bool open;


    protected void Awake()
    {
        menu.SetActive(false);
        open = false;
    }

    protected void OpenMenu()
    {
        Pause();
        menu.SetActive(true);
        open = true;
        HideHint();
    }

    protected void CloseMenu()
    {
        Resume();
        menu.SetActive(false);
        open = false;

    }


}
