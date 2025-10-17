using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public float moveSpeed = 10f;
    private float movement = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal") * moveSpeed;
    }

    void FixedUpdate()
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
