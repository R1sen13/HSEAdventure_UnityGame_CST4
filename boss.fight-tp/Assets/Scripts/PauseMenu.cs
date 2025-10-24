using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public bool isPaused = false;

    void Start()
    {
        pauseMenu.SetActive(false);    
        player.isPaused = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                ResumeGame();
            }
            else{
                PauseGame();
            }
        }
    }

    public void PauseGame(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        player.isPaused = true;
        BossCat.isPaused = true;
        Book.isPaused = true;
    }

    public void ResumeGame(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        player.isPaused = false;
        BossCat.isPaused = false;
        Book.isPaused = false;
    }

    public void Restart(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene"); //ЗДЕСЬ МЕНЯЕТЕ НАЗВАНИЕ С Game НА НАЗВАНИЕ ВАШЕЙ СЦЕНЫ
    }
    public void GoToMainMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

}
