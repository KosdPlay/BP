using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // �������� ����� �������
    [SerializeField] private float lifetime = 2f; // ����� ����� �������
    [SerializeField] private int damage = 10; // ����, ������� ������ �������
    [SerializeField] private GameObject effect;

    private void Start()
    {
        // ��������� �������� ��� ����������� ������� ����� lifetime ������
        StartCoroutine(DestroyAfterLifetime());
    }

    private void Update()
    {
        // ����������� ������� ����� �� �����������
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private IEnumerator DestroyAfterLifetime()
    {
        // ����� lifetime ������, ����� ���������� ������
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ��������� ������������ � ������� ���������
        if (other.CompareTag("Player"))
        {
            // ���� ����������� � �������, ������� ����� TakeDamage � ������
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            // ���������� ������ ����� ������������ � �������
            Destroy(gameObject);
        }
        else
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
