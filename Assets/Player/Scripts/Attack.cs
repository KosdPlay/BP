using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    [SerializeField] private int damage = 10;
    [SerializeField] private float attackCooldown = 0.5f;

    private float lastAttackTime;

    [SerializeField]private bool handsBusy =  false;
    private bool isAttacking = false;
    private bool damageDone = false;

    [SerializeField]private bool canAttack = true;

    private void Start()
    {
        lastAttackTime = -attackCooldown;
    }

    private void FixedUpdate()
    {
        if (canAttack && Time.time - lastAttackTime >= attackCooldown && !handsBusy)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Debug.Log(handsBusy);
                StartCoroutine(PerformAttack());
            }
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("&");
            if (isAttacking && damageDone == false && collision.GetComponent<WaspController>() != null)
            {
                Debug.Log("Бац");
                damageDone = true;
                StartCoroutine(Hit(collision));
            }
        }
    }


    private IEnumerator Hit(Collider2D collision)
    {
        yield return new WaitForSeconds(0.5f);
        collision.GetComponent<WaspController>().TakeDamage(damage);

    }

    private IEnumerator PerformAttack()
    {
        canAttack = false;
        damageDone = false;
        isAttacking = true;
        yield return new WaitForSeconds(1f);
        isAttacking = false;

        lastAttackTime = Time.time;
        canAttack = true; 
    }

    public bool GetCanAttack()
    {
        return canAttack;
    }

    public void SetDamage()
    {
        damage = 20;
    }

    public bool GetIsAttacking()
    {
        return isAttacking;
    }

    public bool GetHandsBusy()
    {
        return handsBusy;
    }

    public void OccupyHands()
    {
        handsBusy = true;
    }

    public void FreeHands()
    {
        handsBusy = false;
    }
}
