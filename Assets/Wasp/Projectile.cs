using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Скорость полёта снаряда
    [SerializeField] private float lifetime = 2f; // Время жизни снаряда
    [SerializeField] private int damage = 10; // Урон, который снаряд наносит
    [SerializeField] private GameObject effect;

    private void Start()
    {
        // Запустить корутину для уничтожения снаряда через lifetime секунд
        StartCoroutine(DestroyAfterLifetime());
    }

    private void Update()
    {
        // Перемещение снаряда вперёд по направлению
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private IEnumerator DestroyAfterLifetime()
    {
        // Ждать lifetime секунд, затем уничтожить снаряд
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Обработка столкновения с другими объектами
        if (other.CompareTag("Player"))
        {
            // Если столкнулись с игроком, вызвать метод TakeDamage у игрока
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            // Уничтожить снаряд после столкновения с игроком
            Destroy(gameObject);
        }
        else
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
