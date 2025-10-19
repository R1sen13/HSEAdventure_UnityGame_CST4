using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioSource sound;
    public static bool isPaused = false;

    private GameManager gameManager;

    void Start()
    {
        pauseMenu.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        // Синхронизируем все флаги паузы
        Parallax_new.isPaused = true;
        Pipes.isPaused = true;
        Vorona_script.isPaused = true;
        sound.Play();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);

        // Восстанавливаем timeScale только если игра была начата
        if (gameManager.IsGameStarted())
        {
            Time.timeScale = 1f;
            isPaused = false;
            // Синхронизируем все флаги паузы
            Parallax_new.isPaused = false;
            Pipes.isPaused = false;
            Vorona_script.isPaused = false;
        }
        else
        {
            // Если игра не начата, остаемся в меню с Time.timeScale = 0
            isPaused = false;
        }

        sound.Play();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("SampleScene");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("Menu");
    }
}