using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private LogicManager logic;
    public static bool isPaused;
    public MusicManager soundPlayer;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
        soundPlayer = GameObject.FindWithTag("MusicManager").GetComponent<MusicManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // произошла жесткая коллизия с игроком :c
            soundPlayer.PlayHit();
            logic.isGameOver = true;
        }
    }
}
