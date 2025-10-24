using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class LogicManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image fadePanel;
    public float fadeSpeed = 1f;
    public bool isGameOver = false;
    public bool isStart = true;
    public static bool isPaused;
    public PlayerController player;
    public MusicManager soundPlayer;
    void Start()
    {
        SceneManager.LoadScene("NewLevel", LoadSceneMode.Additive);
        player = GameObject.FindWithTag("Player")?.GetComponent<PlayerController>();
        soundPlayer = GameObject.FindWithTag("MusicManager").GetComponent<MusicManager>();

    }


    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {

            if (isGameOver)
            {
                player.Die();
                isStart = false;
                Color color = fadePanel.color;
                color.a += Time.deltaTime * fadeSpeed;
                fadePanel.color = color;

                if (color.a >= 1f)
                {
                    RestartGame();
                }
            }

            if (isStart)
            {
                Color color = fadePanel.color;
                color.a -= Time.deltaTime * fadeSpeed;
                fadePanel.color = color;
                if (color.a <= 0f)
                {
                    isStart = false;
                }

            }
        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

// сюда подтянется игрок автоматически

