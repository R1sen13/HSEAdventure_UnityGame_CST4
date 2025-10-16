using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    [Header("Timer Settings")]
    public bool autoStart = true;
    public bool showMilliseconds = false;
    
    [Header("UI References")]
    public Text timerText;
    
    // Переменные таймера
    private float elapsedTime = 0f;
    private bool isRunning = false;
    
    // Событие для обновления времени (опционально)
    public static event Action<string> OnTimeUpdated;
    
    private void Start()
    {
        if (autoStart)
        {
            StartTimer();
        }
        
        // Инициализация текста
        if (timerText != null)
        {
            UpdateTimerDisplay();
        }
    }
    
    private void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }
    
    // Запуск таймера
    public void StartTimer()
    {
        isRunning = true;
    }
    
    // Пауза таймера
    public void PauseTimer()
    {
        isRunning = false;
    }
    
    // Остановка и сброс таймера
    public void StopTimer()
    {
        isRunning = false;
        elapsedTime = 0f;
        UpdateTimerDisplay();
    }
    
    // Сброс таймера без остановки
    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateTimerDisplay();
    }
    
    // Обновление отображения таймера
    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = FormatTime(elapsedTime);
        }
        
        // Вызов события (опционально)
        OnTimeUpdated?.Invoke(FormatTime(elapsedTime));
    }
    
    // Форматирование времени в читаемый вид
    public string FormatTime(float time)
    {
        int hours = (int)(time / 3600);
        int minutes = (int)((time % 3600) / 60);
        int seconds = (int)(time % 60);
        int milliseconds = (int)((time * 1000) % 1000);
        
        if (showMilliseconds)
        {
            return hours > 0 
                ? $"{hours:00}:{minutes:00}:{seconds:00}.{milliseconds:000}"
                : $"{minutes:00}:{seconds:00}.{milliseconds:000}";
        }
        else
        {
            return hours > 0 
                ? $"{hours:00}:{minutes:00}:{seconds:00}"
                : $"{minutes:00}:{seconds:00}";
        }
    }
    
    // Получение текущего времени в секундах
    public float GetCurrentTime()
    {
        return elapsedTime;
    }
    
    // Получение времени в формате TimeSpan
    public TimeSpan GetCurrentTimeSpan()
    {
        return TimeSpan.FromSeconds(elapsedTime);
    }
    
    // Проверка, запущен ли таймер
    public bool IsTimerRunning()
    {
        return isRunning;
    }
}