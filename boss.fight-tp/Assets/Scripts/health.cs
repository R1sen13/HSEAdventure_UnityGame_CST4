using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("Настройки здоровья")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI")]
    public GameObject healthBarPrefab;  // Префаб полоски
    private HealthBar healthBarUI;

    [Header("Опции")]
    public bool destroyOnDeathPlayer = true;
    public bool destroyOnDeath = true;
    public bool isPlayer = false;   // Флаг для различия игрока и босса

    void Awake()
    {
        // 🔹 Всегда начинаем с полного здоровья
        currentHealth = maxHealth;

        // Создаём полоску здоровья
        if (healthBarPrefab != null)
        {
            GameObject bar = Instantiate(healthBarPrefab);
            healthBarUI = bar.GetComponent<HealthBar>();
            healthBarUI.SetTarget(transform);

            // 🔹 Обновляем полоску сразу на полное значение
            healthBarUI.UpdateHealth(currentHealth, maxHealth);
        }
    }

    // Получение урона
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBarUI != null)
            healthBarUI.UpdateHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
            Die();
    }

    // Восстановление здоровья
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBarUI != null)
            healthBarUI.UpdateHealth(currentHealth, maxHealth);
    }

    private void Die()
    {


        // Проверяем, кто умер (игрок или босс)
        if (isPlayer)
        {
            PlayerDie();  // Если это игрок, перезапускаем уровень
        }
        else
        {
            BossDie();  // Если это босс, просто уничтожаем его
        }
    }

    // Логика смерти игрока
    private void PlayerDie()
    {
        Debug.Log("Player has died!");

        // Можно добавить дополнительные эффекты смерти, если нужно

        // Перезапуск уровня при смерти игрока
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Загружаем текущую сцену заново
    }

    // Логика смерти босса
    private void BossDie()
    {
        Debug.Log("Boss has died!");

        // Уничтожаем объект босса
        Destroy(gameObject); // Удаляем объект босса с игры
    }
}
