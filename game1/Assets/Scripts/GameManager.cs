using System;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public Vorona_script player;
    public Text scoreText;
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject winImage;
    public int score;
    public GameObject Spawner;
    public GameObject Background; 

    private void HideGameElements()
    {
        // Скрываем спавнер труб
        if (Spawner != null)
            Spawner.SetActive(false);

        // Скрываем фон (если нужно)
        if (Background != null)
            Background.SetActive(false);

        // Скрываем UI элементы игры
        gameOver.SetActive(false);
        winImage.SetActive(false);
    }

    private void ShowMenuElements()
    {
        // Показываем только счётчик, ворону и кнопку Play
        if (scoreText != null)
            scoreText.gameObject.SetActive(true);

        if (player != null)
            player.gameObject.SetActive(true);

        if (playButton != null)
            playButton.SetActive(true);
    }
    private void Awake()
    {
        Application.targetFrameRate = 60;
        GameObject spawner = GameObject.FindGameObjectWithTag("Spawner");
        if (spawner != null)
            spawner.SetActive(false);

        HideGameElements();
        ShowMenuElements();
        Pause();
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);
        winImage.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void Pause()
    {

        Time.timeScale = 0f;
        player.enabled = false;
    }
    public void GameOver()
    {
        gameOver.SetActive(true);
        playButton.SetActive(true);

        Pause();
    }

    public void WinGame()
    {
        winImage.SetActive(true); 
        playButton.SetActive(true);
        Pause();
    }
    public void IncreaseScore()
    {
        score = score + 10;
        scoreText.text = score.ToString();
        if (score == 300)
        {
            WinGame(); 
        }
    }
}
