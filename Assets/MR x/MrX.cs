using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrX : MonoBehaviour
{
    [SerializeField] private Transform finish;
    [SerializeField] private GameObject X;
    [SerializeField] private float speed;
    [SerializeField] private int damage;

    // Update is called once per frame
    void Update()
    {
        X.transform.position = Vector2.MoveTowards(X.transform.position, finish.position, speed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

        }
    }
}
