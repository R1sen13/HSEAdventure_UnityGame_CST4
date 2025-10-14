using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public float offsetX = 3f;
    
    private float highestX;

    void Start()
    {
        if (target != null)
        {
            highestX = target.position.x;
        }
    }
    
    void LateUpdate()
    {
        if (target == null) return;
        
        // Обновляем самую правую позицию
        if (target.position.x > highestX)
        {
            highestX = target.position.x;
        }
        
        // Камера следует только вперед
        Vector3 newPos = new Vector3(highestX + offsetX, transform.position.y, transform.position.z);
        transform.position = newPos;
    }
}