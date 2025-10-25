using UnityEngine;

public class DiplomPrefab : MonoBehaviour
{
    public GameObject Game_over;  // Ссылка на объект меню Game Over

    // Этот метод срабатывает при входе в триггер
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что объект, с которым столкнулся предмет — это игрок
        if (other.CompareTag("Player"))
        {
            // Если столкнулся с игроком, убираем предмет
            Destroy(gameObject);

            // Появляем меню Game Over
            if (Game_over != null)
            {
                Game_over.SetActive(true); // Показываем меню Game Over
                Time.timeScale = 0f; // Замораживаем игру
            }
        }
    }
}