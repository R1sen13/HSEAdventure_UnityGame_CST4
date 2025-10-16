using UnityEngine;

public class BossCat : MonoBehaviour
{
    public enum BossState { Idle, Walk, Attack, Throw, Jump }
    private BossState currentState = BossState.Idle;

    [Header("Основные параметры")]
    public float moveSpeed = 2f;
    public float stopDistance = 3f;     // дистанция, где кот останавливается
    public float attackRange = 2f;      // радиус удара лапой
    public float detectionRange = 10f;
    public Animator anim;

    [Header("Атака лапой")]
    public float pawCooldown = 2f;
    public int pawDamage = 10;
    public Transform pawPoint;          // пустышка у лапы
    public LayerMask playerLayer;       // слой игрока
    private float lastPawAttack;

    [Header("Атака книгой")]
    public GameObject bookPrefab;
    public Transform throwPoint;
    public float throwForce = 7f;
    public float bookCooldown = 4f;
    private float nextBookTime;

    [Header("Прыжки")]
    public float jumpForce = 7f;
    public float jumpInterval = 6f;
    private float nextJumpTime;

    [Header("Компоненты")]
    public Rigidbody2D rb;
    public Transform player;

    private bool facingRight = true;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (player == null && GameObject.FindWithTag("Player") != null)
            player = GameObject.FindWithTag("Player").transform;

        nextBookTime = Time.time + bookCooldown;
        nextJumpTime = Time.time + jumpInterval;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Смена состояния
        if (distanceToPlayer > detectionRange)
        {
            SetState(BossState.Idle);
        }
        else if (distanceToPlayer > stopDistance)
        {
            SetState(BossState.Walk);
        }
        else
        {
            SetState(BossState.Attack);
        }

        // Поведение
        switch (currentState)
        {
            case BossState.Idle:
                StopMoving();
                break;

            case BossState.Walk:
                MoveTowardsPlayer();
                break;

            case BossState.Attack:
                StopMoving();
                TryPawAttack();
                break;
        }

        // Бросок книги
        if (Time.time >= nextBookTime)
        {
            ThrowBook();
            nextBookTime = Time.time + bookCooldown;
        }

        // Прыжок
        if (Time.time >= nextJumpTime)
        {
            Jump();
            nextJumpTime = Time.time + jumpInterval;
        }

        FlipTowardsPlayer();
    }

    private void SetState(BossState newState)
    {
        if (currentState != newState)
            currentState = newState;
    }

    private void MoveTowardsPlayer()
    {
        if (player == null) return;

        float direction = Mathf.Sign(player.position.x - transform.position.x);
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
        anim.SetFloat("move x", Mathf.Abs(rb.linearVelocity.x));
    }

    private void StopMoving()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    private void FlipTowardsPlayer()
    {
        if (player == null) return;
        if (pawPoint != null)
    {
        Vector3 pos = pawPoint.localPosition;
        pos.x = Mathf.Abs(pos.x) * (facingRight ? 1 : -1);
        pawPoint.localPosition = pos;
    }
        bool shouldFaceRight = player.position.x > transform.position.x;
        if (shouldFaceRight != facingRight)
        {
            facingRight = shouldFaceRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void TryPawAttack()
{
    if (Time.time - lastPawAttack < pawCooldown) return;
    lastPawAttack = Time.time;

    // Проверяем, есть ли игрок в радиусе атаки
    Collider2D[] hits = Physics2D.OverlapCircleAll(pawPoint.position, attackRange, playerLayer);

    foreach (Collider2D hit in hits)
    {
        Health playerHealth = hit.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(pawDamage);
            Debug.Log("Boss heat lapoy!");
        }
    }
}


    private void ThrowBook()
    {
        

        if (bookPrefab == null || throwPoint == null) return;

        GameObject book = Instantiate(bookPrefab, throwPoint.position, Quaternion.identity);
        book.layer = LayerMask.NameToLayer("Enemy_attack"); 

        Book bookScript = book.GetComponent<Book>();
        if (bookScript != null)
        {
        bookScript.owner = "Enemy"; // помечаем, что снаряд босса
        }
        Rigidbody2D bookRb = book.GetComponent<Rigidbody2D>();

        if (bookRb != null)
        {
            float direction = facingRight ? 1f : -1f;
            bookRb.linearVelocity = new Vector2(direction * throwForce, 0f);
        }

        Destroy(book, 3f);
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnDrawGizmosSelected()
    {
        if (pawPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pawPoint.position, attackRange);
        }
    }
}
