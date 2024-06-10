using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private Transform up;
    [SerializeField] private bool isUp;
    [SerializeField] private Transform down;
    [SerializeField] private bool isDown;

    private void Start()
    {
        isUp = true;
        isDown = false;
    }

    private void FixedUpdate()
    {
        if (isUp)
        {
            transform.position = Vector2.MoveTowards(transform.position, down.position, 2 * Time.deltaTime);
        }
        if(isDown)
        {
            transform.position = Vector2.MoveTowards(transform.position, up.position, 2 * Time.deltaTime);
        }
        if (Vector2.Distance(transform.position, up.transform.position) < 0.5f)
        {
            isUp = true;
            isDown = false;
        }
        if (Vector2.Distance(transform.position, down.transform.position) < 0.5f)
        {
            isUp = false;
            isDown = true;
        }
    }
}
