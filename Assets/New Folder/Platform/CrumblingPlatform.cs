using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrumblingPlatform : MonoBehaviour
{
    [SerializeField] private float timeDestroy = 1.5f;
    [SerializeField] private float timeRecovery = 6f;
    [SerializeField] private GameObject platform;
    private bool platformCollapsed;

    //[SerializeField] private GameObject effect;

    private bool isDestroy = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (platformCollapsed == false)
            {

                PlatformCollapse();


            }
        }
    }

    private void Destroy()
    {
        if (isDestroy == false)
        {
            isDestroy = true; platformCollapsed = true;
            //Instantiate(effect, platform.transform.position, Quaternion.identity);
            platform.SetActive(false);
        }
    }
    private void Recovery()
    {
        platformCollapsed = false;
        isDestroy = false; platform.SetActive(true);
    }
    private void PlatformCollapse()
    {
        Debug.Log("0");
        Invoke("Destroy", timeDestroy); Invoke("Recovery", timeDestroy + timeRecovery);
    }
}