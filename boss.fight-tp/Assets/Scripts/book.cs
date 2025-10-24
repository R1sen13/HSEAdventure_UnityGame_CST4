using UnityEngine;

public class Book : MonoBehaviour
{
    [Header("Параметры книги")]
    public float damage = 25f;
    public float lifetime = 3f;         
    public float rotationSpeed = 720f;   
    public string owner;

    private Rigidbody2D rb;
    private Transform visual;
    private float direction = 1f;
    private float speed;
     
    [Header("Пауза")]
    public static bool isPaused;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.freezeRotation = true;
            rb.gravityScale = 0;
        }
        if (transform.childCount == 0)
        {
            SpriteRenderer original = GetComponent<SpriteRenderer>();
            GameObject visualObj = new GameObject("Visual");
            visualObj.transform.SetParent(transform);
            visualObj.transform.localPosition = Vector3.zero;

            SpriteRenderer sr = visualObj.AddComponent<SpriteRenderer>();
            sr.sprite = original.sprite;
            sr.sortingLayerID = original.sortingLayerID;
            sr.sortingOrder = original.sortingOrder;
            original.enabled = false;

            visual = visualObj.transform;
        }
        else
        {
            visual = transform.GetChild(0);
        }
    }

    public void Launch(float dir, float spd)
    {
        direction = dir;
        speed = spd;
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if(!isPaused){
            transform.position += new Vector3( direction * speed * Time.deltaTime, 0, direction * speed * Time.deltaTime);

            if (visual != null){
                visual.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    // если столкнулись с кем-то, у кого тег отличается от владельца
    if (!collision.gameObject.CompareTag(owner))
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }

        // уничтожаем книгу при столкновении
        Destroy(gameObject);
    }
    }
}