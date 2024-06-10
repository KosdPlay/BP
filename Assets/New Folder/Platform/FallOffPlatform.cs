using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOffPlatform : MonoBehaviour
{
    [SerializeField] private BoxCollider2D platform;

    private void Start()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.S))
            {
                Debug.Log("iuhj");
                platform.isTrigger = true;
            }
        }
    }
}
