using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed = 0.15f;  // скорость движения облака

    private Vector3 localPos;

    void Start()
    {
        localPos = transform.localPosition; // стартовая позиция относительно камеры
    }

    void Update()
    {
        // двигаем облако влево относительно камеры
        localPos += Vector3.left * speed * Time.deltaTime;
        transform.localPosition = localPos;

        // если ушло за левую границу камеры — уничтожаем
        if (localPos.x < -5f)
            Destroy(gameObject);
    }
}