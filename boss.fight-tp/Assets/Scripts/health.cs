using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Настройки здоровья")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Настройки интерфейса")]
    public GameObject healthBarPrefab; // префаб полоски здоровья
    private HealthBar healthBarUI;     // ссылка на скрипт HealthBar

    public bool destroyOnDeath = true;
    private bool isDead = false;

    void Awake()
{
    currentHealth = maxHealth;

    if (healthBarPrefab != null)
    {
        GameObject bar = Instantiate(healthBarPrefab, Vector3.zero, Quaternion.identity);
        healthBarUI = bar.GetComponent<HealthBar>();
        healthBarUI.SetTarget(transform);
        healthBarUI.UpdateHealth(currentHealth, maxHealth);
    }
}


    void Start()
    {
        currentHealth = maxHealth;

        // создаём полоску здоровья, если задан префаб
        if (healthBarPrefab != null)
{
    GameObject bar = Instantiate(healthBarPrefab, Vector3.zero, Quaternion.identity);
    healthBarUI = bar.GetComponent<HealthBar>();
    healthBarUI.SetTarget(transform);
    healthBarUI.UpdateHealth(currentHealth, maxHealth);
}

    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // обновляем полоску
        if (healthBarUI != null)
            healthBarUI.UpdateHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        if (isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBarUI != null)
            healthBarUI.UpdateHealth(currentHealth, maxHealth);
    }

    private void Die()
    {
        isDead = true;

        if (healthBarUI != null)
            Destroy(healthBarUI.gameObject);

        if (destroyOnDeath)
            Destroy(gameObject);
    }

    public float GetHealth() => currentHealth;
}
