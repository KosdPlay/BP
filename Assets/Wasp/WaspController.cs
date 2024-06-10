using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaspController : MonoBehaviour
{
    private float speed;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float MinSpeed;

    [SerializeField] private float distanceZone;
    [SerializeField] private float maxDistanceZone;
    [SerializeField] private Vector3 startScale;

    [SerializeField] private Image lineBar;

    private Rigidbody2D rb;

    private GameObject[] target;

    private Vector2 startPosition;

    private bool angry;
    private bool flipBot;

    private Animator animator;

    [SerializeField] private float StartTimeDamage;
    [SerializeField] private float TimeDamage;
    [SerializeField] private Player player;
    [SerializeField] private int Damage;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform point3;
    [SerializeField] private Transform point2;
    [SerializeField] private Transform point0;
    [SerializeField] private Transform point4;
    [SerializeField] private Transform point5;
    private bool corstert = false;
    private bool statestert = false;

    private bool pl_in_zone = false;

    private int state = 0;

    [SerializeField] private int hp = 500;

    private byte inplace = 1;

    private List<int> attacs = new List<int>();

    private Transform flipTarget;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = UnityEngine.Random.Range(MinSpeed, MaxSpeed);
        animator = GetComponent<Animator>();

        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        startPosition = new Vector2(transform.position.x, transform.position.y);

        startScale = transform.localScale;


    }

    private void UpdateUI()
    {
        lineBar.fillAmount = (float)hp / 100;
    }

    private void FixedUpdate()
    {
        if (hp <= 0)
        {
            Death();
        }
        CheckingStatus();
    }

    private void Angry()
    {
        if (Vector2.Distance(transform.position, target[0].transform.position) < maxDistanceZone && !pl_in_zone)
        {
            pl_in_zone = true;
            if (!statestert) { 
                StartCoroutine(State());
                statestert= true;
            }

        }
        else if (Vector2.Distance(transform.position, target[0].transform.position) >= maxDistanceZone && pl_in_zone)
        {
            transform.position = this.transform.position;
            Idle();
            pl_in_zone = false;
        }
    }


    public void Idle()
    {
        transform.position = Vector2.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
        animator.SetBool("Idle", false);
    }

    private void Flip()
    {
        if (transform.position.x < flipTarget.position.x)
        {
            transform.localScale = new Vector3(startScale.x, startScale.y, startScale.z);
            flipBot = true;
        }
        else if (transform.position.x > flipTarget.position.x)
        {
            transform.localScale = new Vector3(startScale.x * -1, startScale.y, startScale.z);
            flipBot = false;
        }
    }
    private IEnumerator State()
    {
        while (true)
        {
            if(attacs.Count == 0)
            {
                attacs.Add(0);
                attacs.Add(1);
                attacs.Add(2);
                attacs.Add(3);
            }
            state = attacs[UnityEngine.Random.Range(0, attacs.Count)];
            attacs.Remove(state);
            Debug.Log(state);
            if (!corstert) { 
                    StartCoroutine(Atack1());
                    StartCoroutine(Atack2());
                    StartCoroutine(Atack3());
                    StartCoroutine(Atack4());
                    StartCoroutine(Atack5());
                    corstert = true;
            }
            if (state != 3)
            {
                yield return new WaitForSeconds(10.0f);
            }
            else
            {
                yield return new WaitForSeconds(5.0f);
            }
            state = 4;
            yield return new WaitForSeconds(5.0f);
        }

    }


    public void TakeDamage(int damage)
    {
        hp -= damage;
        UpdateUI();
        if (hp <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        //CutsceneManager.Instance.StartCutscene("CS_2");

        Destroy(this.gameObject, 1f);
    }

    private void Atack()
    {
        if (TimeDamage <= 0)
        {
            TimeDamage = StartTimeDamage;
            Player playerScript = player.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(Damage);
            }

        }
        else
        {
            TimeDamage -= Time.deltaTime;
        }
    }
    public void CheckingStatus()
    {

        if (player != null)
        {
            angry = true; target = GameObject.FindGameObjectsWithTag("Player");
        }
        else
        {
            target = null; angry = false;
        }
        if (angry)
        {
            Angry(); 
        }
        else
        {
            Idle();
        }
    }
    private IEnumerator Atack1()
    {
        while (true) {
            if (state == 0)
            {
            while (Vector2.Distance(transform.position, target[0].transform.position) > distanceZone && Vector2.Distance(transform.position, target[0].transform.position) < maxDistanceZone)
            {
                flipTarget = target[0].transform;
                Flip();
                transform.position = Vector2.MoveTowards(transform.position, target[0].transform.position + new Vector3(0, 2, 0), speed * Time.deltaTime);
                animator.SetBool("Idle", false);
                    if (state != 0) break;
                    yield return null;

            }
            while (Vector2.Distance(transform.position, target[0].transform.position) <= distanceZone)
            {
                transform.position = this.transform.position;
                animator.SetBool("Idle", true);
                Atack();
                    if (state != 0) break;
                    yield return null;
            }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator Atack2()
    {
        while (true)
        {

            if (state == 1)
            {
                while (Vector2.Distance(transform.position, point3.position) > 3f)
                {
                    flipTarget = point3;
                    Flip();
                    transform.position = Vector2.MoveTowards(transform.position, point3.position + new Vector3(0, 2, 0), speed * Time.deltaTime);
                    animator.SetBool("Idle", false);
                    yield return null;
                    if (state != 1) break;
                }
                if (Vector2.Distance(transform.position, point3.position) <= 3f)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Instantiate(bulletPrefab, point2.position + new Vector3(UnityEngine.Random.Range(-10f, 10f), 0, 0), new Quaternion(0f, 0f, 1f, 1f));
                    }
                    yield return new WaitForSeconds(2.0f);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator Atack3()
    {
        while (true)
        {
            if (state == 2)
            {
                while (Vector2.Distance(transform.position, point2.position) > 3f)
                {
                    flipTarget = point2;
                    Flip();
                    transform.position = Vector2.MoveTowards(transform.position, point2.position + new Vector3(0, 2, 0), speed * Time.deltaTime);
                    animator.SetBool("Idle", false);
                    yield return null;
                    if (state != 2) break;
                }
                if (Vector2.Distance(transform.position, point2.position) <= 4f)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Quaternion rotat = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(25, 155));
                        Instantiate(bulletPrefab, firepoint.position, rotat);


                    }
                    yield return new WaitForSeconds(2.0f);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator Atack4()
    {
        while (true)
        {
            if (state == 4)
            {
                while (Vector2.Distance(transform.position, point0.position) > 1f)
                {
                    flipTarget = point0;
                    Flip();
                    transform.position = Vector2.MoveTowards(transform.position, point0.position + new Vector3(0, 2, 0), speed * Time.deltaTime);
                    animator.SetBool("Idle", false);
                    yield return null;
                    if (state != 4) break;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator Atack5()
    {
        while (true)
        {
            if (state == 3)
            {
                Debug.Log(inplace);
                if (inplace == 1)
                {
                    flipTarget = point5;
                    Flip();
                    while (Vector2.Distance(transform.position, point5.position) > 2f)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, point5.position + new Vector3(0, 2, 0), speed * Time.deltaTime);
                        animator.SetBool("Idle", false);
                        yield return null;
                        if (state != 3) break;
                        if (Vector2.Distance(transform.position, target[0].transform.position) <= distanceZone)
                        {
                            Atack();
                        }
                    }
                    inplace = 2;
                }
                if (inplace == 2)
                {
                    flipTarget = point4;
                    Flip();
                    while (Vector2.Distance(transform.position, point4.position) > 2f)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, point4.position + new Vector3(0, 2, 0), speed * Time.deltaTime);
                        animator.SetBool("Idle", false);
                        yield return null;
                        if (state != 3) break;
                        if (Vector2.Distance(transform.position, target[0].transform.position) <= distanceZone)
                        {
                            Atack();
                        }
                    }
                    inplace = 1;
                }
                Debug.Log(inplace);
                
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}