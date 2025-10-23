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

        // Приостанавливаем музыку при паузе
        if (gameManager.backgroundMusic != null && gameManager.backgroundMusic.isPlaying)
        {
            gameManager.backgroundMusic.Pause();
        }

        Parallax_new.isPaused = true;
        Pipes.isPaused = true;
        Vorona_script.isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);

        if (gameManager.IsGameStarted())
        {
            Time.timeScale = 1f;
            isPaused = false;

            // Возобновляем музыку при возобновлении игры
            if (gameManager.backgroundMusic != null)
            {
                gameManager.backgroundMusic.UnPause();
            }

            Parallax_new.isPaused = false;
            Pipes.isPaused = false;
            Vorona_script.isPaused = false;
        }
        else
        {
            isPaused = false;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        isPaused = false;

        // При перезапуске включаем игровую музыку
        if (gameManager != null)
        {
            gameManager.PlayGameMusic();
        }

        SceneManager.LoadScene("SampleScene");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;

        // При возврате в меню включаем музыку меню
        if (gameManager != null)
        {
            gameManager.PlayMenuMusic();
        }

        SceneManager.LoadScene("Menu");
    }
}