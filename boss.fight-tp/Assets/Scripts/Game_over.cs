using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject Game_over;  // Ссылка на объект меню Game Over
    public bool isGameOver = false;  // Флаг, указывающий, что игра завершена
    public GameObject DiplomPrefab;  // Префаб предмета, который выпадает с босса

    void Start()
    {
        // Скрываем меню Game Over по умолчанию
        Game_over.SetActive(false);
    }

    // Метод для отображения меню Game Over
    public void ShowGameOverMenu()
    {
        // Показываем меню Game Over
        Game_over.SetActive(true);

        // Останавливаем время (замораживаем игру)
        Time.timeScale = 0f;

        // Можно добавить дополнительные эффекты при завершении игры
    }

    // Кнопка для перезапуска уровня
    public void RestartGame()
    {
        // Снимаем заморозку времени
        Time.timeScale = 1f;

        // Перезагружаем текущую сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Кнопка для выхода в главное меню
    public void GoToMainMenu()
    {
        // Снимаем заморозку времени
        Time.timeScale = 1f;

        // Загружаем сцену главного меню
        SceneManager.LoadScene("Menu");  // Меняйте "Menu" на название вашей сцены меню
    }
}