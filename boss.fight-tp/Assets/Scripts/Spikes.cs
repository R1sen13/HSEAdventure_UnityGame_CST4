using UnityEngine;

public class Spike : MonoBehaviour
{
    public int damage = 100;  // Урон, который наносят шипы (можно настроить в инспекторе)

    // Проверка на столкновение с игроком
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Находим игрока и применяем урон
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Вызываем функцию получения урона в скрипте игрока
            }
        }
    }
}