using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public static bool isPaused;
    public float speed = 1.5f;        // скорость движения
    public float amplitude = 2f;    // амплитуда

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (!isPaused)
        {
            // колебание по оси Y
            float offset = Mathf.PingPong(Time.time * speed, amplitude);
            transform.position = startPos + Vector3.up * offset;
        }
    }
}