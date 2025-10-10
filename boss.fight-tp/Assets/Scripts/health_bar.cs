using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Vector3 offset = new Vector3(0, 1.5f, 0);
    private Transform target;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        // 🔧 Попробуем найти Canvas, если объект создан не внутри него
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null && transform.parent != canvas.transform)
        {
            transform.SetParent(canvas.transform, false);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void UpdateHealth(float current, float max)
    {
        if (slider != null)
            slider.value = current / max;
    }

    void LateUpdate()
    {
        if (target == null || cam == null) return;

        Vector3 screenPos = cam.WorldToScreenPoint(target.position + offset);
        transform.position = screenPos;
    }
}
