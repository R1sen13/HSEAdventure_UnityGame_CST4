using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;            // Слайдер заполнения
    public Vector3 offset = new Vector3(0, 1.5f, 0); // Смещение над персонажем

    private Transform target;        // За кем следим
    private Canvas canvas;

    [Header("Пауза")]
    public static bool isPaused;

    // Привязка полоски к персонажу
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // Обновление текущего значения полоски
    public void UpdateHealth(float current, float max)
    {
        if (slider != null)
            slider.value = current / max;
    }

    void Start()
    {
        // Найти Canvas на сцене и сделать дочерним объектом
        canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
            transform.SetParent(canvas.transform, false);
        else
            Debug.LogWarning("Canvas не найден на сцене!");
    }

    void LateUpdate()
    {
        if(!isPaused){
            if (target == null || canvas == null) return;

            // Преобразуем мировые координаты в экранные
            Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position + offset);
            transform.position = screenPos;
            }
    }
}
