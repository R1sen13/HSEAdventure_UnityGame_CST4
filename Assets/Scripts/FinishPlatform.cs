using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPlatform : MonoBehaviour
{
    public GameObject uiPanel;       // сама панель
    public Animator panelAnimator;   // Animator
    private bool playerOnPlatform = false;
    public MusicManager soundPlayer;

    //
    void Start()
    {
        uiPanel = GameObject.FindGameObjectWithTag("FinishPanel");
        panelAnimator = uiPanel.GetComponent<Animator>();
        soundPlayer = GameObject.FindWithTag("MusicManager").GetComponent<MusicManager>();

        if (uiPanel != null)
            uiPanel.SetActive(false);
    }

    // Срабатывает при касании игрока
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerOnPlatform)
        {
            playerOnPlatform = true;

            // показываем панель
            if (uiPanel != null)
                uiPanel.SetActive(true);

            // запуск анимации FadeIn панели
            if (panelAnimator != null)
            {
                panelAnimator.SetBool("FadeStart", true);
            }
        }
    }

    // Обработка Enter для смены сцены
    void Update()
    {
        if (playerOnPlatform && Input.GetKeyDown(KeyCode.Return))
        {
            soundPlayer.PlayEnter();
            // Смена сцены
            SceneManager.LoadScene("NewLevel"); // Егор, поменяй здесь "NewLevel" на мультик или новый уровень
        }
    }
}