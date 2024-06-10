using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MenuBase
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HintConclusion("ֽאזלטעו E");
            if (Input.GetKey(KeyCode.E) && !open)
            {
                OpenMenu();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HideHint();
        }
    }
    private void Update()
    {
        if (open == true && Input.GetKeyUp(KeyCode.Escape))
        {
            CloseMenu();
        }
    }

    new public void CloseMenu()
    {
        Resume();
        menu.SetActive(false);
        open = false;
    }
}
