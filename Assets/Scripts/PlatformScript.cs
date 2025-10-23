using UnityEngine;
using UnityEngine.UIElements;


public class PlatformScript : MonoBehaviour
{
    public float jumpForce = 10f;
    public Transform gameCamera;

    void Start()
    {
        gameCamera = GameObject.FindWithTag("MainCamera")?.transform;

    }

    void Update()
    {
        if (gameObject.transform.position.y < gameCamera.position.y - 5.6f && gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0f)
        {

            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector2 velocity = rb.linearVelocity;
                velocity.y = jumpForce;
                rb.linearVelocity = velocity;



            }

        }
    }

}

