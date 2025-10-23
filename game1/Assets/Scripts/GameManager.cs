using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Vorona_script player;
    public Text scoreText;
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject gameOver2;
    public GameObject winImage1;
    public GameObject winImage2;
    public GameObject winImage3;
    public int score;
    public GameObject Spawner;
    public GameObject Background;

    // Добавьте эти поля для музыки
    public AudioSource backgroundMusic;
    public AudioClip gameMusic;
    public AudioClip menuMusic; // Новая музыка для меню

    private bool gameStarted = false;
    private Vector3 initialPlayerPosition;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        if (player != null)
        {
            initialPlayerPosition = player.transform.position;
        }

        ShowMenuState();
    }

    private void ShowMenuState()
    {
        gameStarted = false;

        // Включаем музыку меню при показе меню
        PlayMenuMusic();

        // Остальной код без изменений...
        if (Spawner != null)
            Spawner.SetActive(false);
        if (Background != null)
            Background.SetActive(false);

        gameOver.SetActive(false);
        gameOver2.SetActive(false);
        winImage1.SetActive(false);
        winImage2.SetActive(false);
        winImage3.SetActive(false);

        if (scoreText != null)
            scoreText.gameObject.SetActive(true);
        if (player != null)
            player.gameObject.SetActive(true);
        if (playButton != null)
            playButton.SetActive(true);

        Time.timeScale = 0f;
    }

    public void Play()
    {
        gameStarted = true;
        score = 0;
        scoreText.text = score.ToString();

        // Переключаем на игровую музыку
        PlayGameMusic();

        // Остальной код без изменений...
        if (player != null)
        {
            player.transform.position = initialPlayerPosition;
            player.ResetBird();
            player.enabled = true;
        }

        playButton.SetActive(false);
        gameOver.SetActive(false);
        gameOver2.SetActive(false);
        winImage1.SetActive(false);
        winImage2.SetActive(false);
        winImage3.SetActive(false);

        if (Spawner != null)
            Spawner.SetActive(true);
        if (Background != null)
            Background.SetActive(true);

        Time.timeScale = 1f;
        PauseMenu.isPaused = false;
        Parallax_new.isPaused = false;
        Pipes.isPaused = false;
        Vorona_script.isPaused = false;

        Pipes[] pipes = FindObjectsOfType<Pipes>();
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void GameOver()
    {
        gameStarted = false;

        // Переключаем обратно на музыку меню при проигрыше
        PlayMenuMusic();

        gameOver.SetActive(true);
        gameOver2.SetActive(true);
        playButton.SetActive(true);
        Time.timeScale = 0f;
        PauseMenu.isPaused = true;
    }

    public void WinGame()
    {
        gameStarted = false;

        // Переключаем обратно на музыку меню при победе
        PlayMenuMusic();

        winImage1.SetActive(true);
        winImage2.SetActive(true);
        winImage3.SetActive(true);
        playButton.SetActive(true);
        Time.timeScale = 0f;
        PauseMenu.isPaused = true;
    }

    // Новые методы для управления музыкой
    public void PlayGameMusic()
    {
        if (backgroundMusic != null && gameMusic != null)
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = gameMusic;
            backgroundMusic.loop = true;
            backgroundMusic.Play();
        }
    }

    public void PlayMenuMusic()
    {
        if (backgroundMusic != null && menuMusic != null)
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = menuMusic;
            backgroundMusic.loop = true;
            backgroundMusic.Play();
        }
    }

    // Метод для принудительной остановки музыки (если нужно)
    public void StopMusic()
    {
        if (backgroundMusic != null && backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop();
        }
    }

    // Остальные методы без изменений...
    public bool IsGameStarted()
    {
        return gameStarted;
    }

    public void IncreaseScore()
    {
        score = score + 5;
        scoreText.text = score.ToString();
        if (score == 300)
        {
            WinGame();
        }
    }
}