using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Настройки здоровья")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI")]
    public GameObject healthBarPrefab;  // Префаб полоски
    private HealthBar healthBarUI;

    [Header("Опции")]
    public bool destroyOnDeath = true;

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

    // Смерть персонажа
    private void Die()
    {
        if (healthBarUI != null)
            Destroy(healthBarUI.gameObject);

        if (destroyOnDeath)
            Destroy(gameObject);
    }

    public float GetHealth() => currentHealth;
}
