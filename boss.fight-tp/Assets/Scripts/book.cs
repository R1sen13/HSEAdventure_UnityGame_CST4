using UnityEngine;

public class Book : MonoBehaviour
{
    [Header("Параметры книги")]
    public float damage = 25f;
    public float lifetime = 3f;         
    public float rotationSpeed = 720f;   
    public GameObject owner;

    private Rigidbody2D rb;
    private Transform visual;
    private float direction = 1f;
    private float speed;
     

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // полностью отключаем влияние физики на вращение и гравитацию
            rb.freezeRotation = true;
            rb.gravityScale = 0;
        }

        // создаём визуальный слой для вращения (если его нет)
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
        // движение вручную (никакой физики)
        transform.position += new Vector3( direction * speed * Time.deltaTime, 0, direction * speed * Time.deltaTime);

        // вращение только визуала
        if (visual != null)
            visual.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == owner) return;
        // Проверяем, есть ли компонент Health у того, во что попали
        Health targetHealth = collision.GetComponent<Health>();

        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
            Debug.Log($"{collision.name} получил {damage} урона от книги");
        }

        Destroy(gameObject); // книга исчезает при столкновении
    }
}
