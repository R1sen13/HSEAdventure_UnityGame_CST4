using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Text scoreText;
    private float start_pos = 0f;
    public int scoreMultiplicator = 10;
    public static bool isPaused;
    private int score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindWithTag("Player")?.transform;
        start_pos = transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {

            if (transform.position.y > start_pos)
            {
                float difference = (transform.position.y - start_pos) * scoreMultiplicator;
                score += (int)difference;
                scoreText.text = score.ToString();
                start_pos = transform.position.y;



            }
        }

    }

    void LateUpdate()
    {
        if (!isPaused)
        {
            if (target.position.y > transform.position.y)
            {
                Vector3 newPos = new Vector3(transform.position.x, target.position.y, transform.position.z);
                transform.position = newPos;
            }
        }
    }

}
