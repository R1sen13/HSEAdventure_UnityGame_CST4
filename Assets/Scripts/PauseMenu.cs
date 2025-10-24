using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public MusicManager soundPlayer;
    public bool isPaused = false;
    public LogicManager logic;

    void Start()
    {
        pauseMenu.SetActive(false);
        soundPlayer = GameObject.FindWithTag("MusicManager").GetComponent<MusicManager>();


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            soundPlayer.PlayPause();
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
        CameraFollow.isPaused = true;
        CloudMovement.isPaused = true;
        EnemyScript.isPaused = true;
        PlatformScript.isPaused = true;
        PlayerController.isPaused = true;
        MovingEnemyMovement.isPaused = true;
        // StaticEnemyMovement.isPaused = true;
        soundPlayer.PlayPause();
        LogicManager.isPaused = true;
        MusicManager.isPaused = true;

    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        CameraFollow.isPaused = false;
        CloudMovement.isPaused = false;
        EnemyScript.isPaused = false;
        LogicManager.isPaused = false;
        MusicManager.isPaused = false;
        PlatformScript.isPaused = false;
        PlayerController.isPaused = false;
        MovingEnemyMovement.isPaused = false;
        soundPlayer.PlayPause();
        // StaticEnemyMovement.isPaused = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  //ЗДЕСЬ МЕНЯЕТЕ НАЗВАНИЕ С Game НА НАЗВАНИЕ ВАШЕЙ СЦЕНЫ
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HSEDoodle");

    }

}
