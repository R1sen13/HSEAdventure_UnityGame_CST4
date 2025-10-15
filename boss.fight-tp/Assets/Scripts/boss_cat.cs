using UnityEngine;

public class BossCat : MonoBehaviour
{
    public enum BossState { Idle, Walk, Attack, Throw, Jump }
    private BossState currentState = BossState.Idle;

    [Header("Основные параметры")]
    public float moveSpeed = 2f;
    public float stopDistance = 3f;
    public float attackDistance = 2f;
    public float detectionRange = 10f;

    [Header("Атака лапой")]
    public float pawCooldown = 2f;
    public int pawDamage = 10;
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

        currentState = BossState.Idle;
        nextBookTime = Time.time + bookCooldown;
        nextJumpTime = Time.time + jumpInterval;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Выбор состояния
        if (distanceToPlayer <= attackDistance)
            SetState(BossState.Attack);
        else if (distanceToPlayer <= stopDistance)
            SetState(BossState.Idle);
        else if (distanceToPlayer <= detectionRange)
            SetState(BossState.Walk);
        else
            SetState(BossState.Idle);

        // Действия по состоянию
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
                TryPawAttack(distanceToPlayer);
                break;
        }

        // Периодически бросает книги
        if (Time.time >= nextBookTime)
        {
            SetState(BossState.Throw);
            ThrowBook();
            nextBookTime = Time.time + bookCooldown;
        }

        // Иногда прыгает
        if (Time.time >= nextJumpTime)
        {
            Jump();
            nextJumpTime = Time.time + jumpInterval;
        }

        FlipTowardsPlayer();
    }

    // ---------------- ЛОГИКА СОСТОЯНИЙ ----------------

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
    }

    private void StopMoving()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    private void FlipTowardsPlayer()
    {
        if (player == null) return;

        bool shouldFaceRight = player.position.x > transform.position.x;
        if (shouldFaceRight != facingRight)
        {
            facingRight = shouldFaceRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    // ---------------- АТАКИ ----------------

    private void TryPawAttack(float distance)
    {
        if (Time.time - lastPawAttack < pawCooldown) return;

        lastPawAttack = Time.time;

        // Здесь можно добавить эффект или проверку попадания
        if (distance <= attackDistance)
        {
            // Проверяем, есть ли у игрока скрипт Health
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(pawDamage);
            }
        }
    }

    private void ThrowBook()
    {
        if (bookPrefab == null || throwPoint == null) return;

        GameObject book = Instantiate(bookPrefab, throwPoint.position, Quaternion.identity);
        Rigidbody2D bookRb = book.GetComponent<Rigidbody2D>();

        if (bookRb != null)
        {
            float direction = facingRight ? 1f : -1f;
            bookRb.linearVelocity = new Vector2(direction * throwForce, 0f);
        }

        // Уничтожаем снаряд через 3 секунды
        Destroy(book, 3f);
    }

    private void Jump()
    {
        if (rb != null)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
