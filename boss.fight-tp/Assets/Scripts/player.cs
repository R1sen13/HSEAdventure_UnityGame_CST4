using UnityEngine;

public class player : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float jumpForce = 7f;
    public Animator anim;

    [Header("Physics")]
    public Rigidbody2D rb;
    public float rayDistance = 0.7f;
    public bool isGround;

    [Header("Jump Settings")]
    public bool doubleJump = false;

    [Header("Glide Settings")]
    public bool canGlide = true;
    public float glideGravityScale = 0.3f;
    private float defaultGravityScale;

    [Header("Attack Settings")]
    public GameObject bookPrefab;   
    public Transform throwPoint;        
    public float throwForce = 10f;      
    public float attackCooldown = 0.5f; 
    private float lastAttackTime = 0f;  
    private bool facingRight = true;

    private void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        defaultGravityScale = rb.gravityScale;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
        HandleGlide();
        HandleAttack();
    }

    
    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            facingRight = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            facingRight = false;
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
          anim.SetFloat("move x", Mathf.Abs(rb.linearVelocity.x));
        
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, rayDistance, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            isGround = true;
            doubleJump = false;
            canGlide = true;
        }
        else
        {
            isGround = false;
        }
    }

    
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
            else if (!doubleJump && rb.linearVelocity.y < 0)
            {
                doubleJump = true;
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
        anim.SetFloat("move y", Mathf.Abs(rb.linearVelocity.y));
    }

    private void HandleGlide()
    {
        if (!isGround && Input.GetKey(KeyCode.Space) && rb.linearVelocity.y <= 0 && canGlide)
        {
            rb.gravityScale = glideGravityScale;
        }
        else
        {
            rb.gravityScale = defaultGravityScale;
        }
    }

    private void HandleAttack()
    {
        if ((Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0)) && Time.time >= lastAttackTime + attackCooldown)
        {
            ThrowBook();
            lastAttackTime = Time.time;
        }
    }

    private void ThrowBook()
    {
    if (bookPrefab == null || throwPoint == null)
    {
        Debug.LogWarning("Book prefab или ThrowPoint не назначены!");
        return;
    }

    GameObject book = Instantiate(bookPrefab, throwPoint.position, Quaternion.identity);

    float direction = facingRight ? 1f : -1f;
    float bookSpeed = throwForce; 
    Book bookScript = book.GetComponent<Book>();
    if (bookScript != null) {
        bookScript.owner = gameObject; 
        bookScript.Launch(direction, bookSpeed);
    }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayDistance);
        if (throwPoint != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(throwPoint.position, 0.1f);
        }
    }
}
