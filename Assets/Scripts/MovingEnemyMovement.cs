using UnityEditor;
using UnityEngine;

public class MovingEnemyMovement : MonoBehaviour
{
    public float speed = 1f;
    public float distance = 3f;
    private Vector2 startPos;
    private bool movingRight = true;
    SpriteRenderer spriteRenderer;
    public static bool isPaused;
    public float floatSpeed = 1f;   // скорость вертикального покачивания
    public float floatHeight = 0.25f; // амплитуда
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        int chosenDirection = Random.Range(0, 2);
        if (chosenDirection == 0)
        {
            spriteRenderer.flipX = false;
            movingRight = true;

        }
        else
        {
            spriteRenderer.flipX = true;
            movingRight = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            if (movingRight)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
                if (transform.position.x >= 3f)
                {
                    movingRight = false;
                    spriteRenderer.flipX = true;
                }

            }
            else
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
                if (transform.position.x <= -2.5f)
                {
                    movingRight = true;
                    spriteRenderer.flipX = false;
                }

            }
            float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            transform.position = new Vector3(transform.position.x, startPos.y + yOffset, transform.position.z);
        }
    }
}
