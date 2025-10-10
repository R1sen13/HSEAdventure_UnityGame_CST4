using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Игрок
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    [Header("Camera Bounds")]
    public float minX; // Левая граница
    public float maxX; // Правая граница
    public float minY; // Нижняя граница
    public float maxY; // Верхняя граница

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Ограничиваем движение камеры
        float clampX = Mathf.Clamp(smoothedPosition.x, minX, maxX);
        float clampY = Mathf.Clamp(smoothedPosition.y, minY, maxY);

        transform.position = new Vector3(clampX, clampY, transform.position.z);
    }
}
