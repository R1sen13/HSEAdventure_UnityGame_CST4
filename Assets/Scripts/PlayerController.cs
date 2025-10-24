using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(LogicManager))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public static bool isPaused;
    public LogicManager logic;
    public bool isGameOver = false;
    public Sprite aliveSprite;
    public Sprite deadSprite;

    SpriteRenderer spriteRenderer;
    public float moveSpeed = 10f;
    private float movement = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        logic = GetComponent<LogicManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        GetComponent<CapsuleCollider2D>().enabled = true;
        spriteRenderer.sprite = aliveSprite;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
            movement = Input.GetAxis("Horizontal") * moveSpeed;

    }

    public void Die()
    {

        spriteRenderer.sprite = deadSprite;
        GetComponent<CapsuleCollider2D>().enabled = false;
        isGameOver = true;

    }


    void FixedUpdate()
    {
        if (!isPaused)
        {
            if (!isGameOver)
            {
                // Base moving script
                Vector2 velocity = rb.linearVelocity;
                velocity.x = movement;
                rb.linearVelocity = velocity;
                // Flipping the sprite
                if (movement > 0)
                {
                    spriteRenderer.flipX = true;
                }
                else if (movement < 0)
                {
                    spriteRenderer.flipX = false;
                }

            }
        }
    }

}
