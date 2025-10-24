using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static bool isPaused;
    public AudioClip jumpSound;  // звук прыжка
    public AudioClip hitSound;   // звук урона
    public AudioClip enterSound;   // звук конца уровня
    public AudioClip pauseSound;   // звук паузы
    public AudioClip musicClip; // клип с музычкой
    public float volume = 0.5f;
    private AudioSource musicSource;
    private AudioSource sfxSource;

    void Awake()
    {
        // Проверка, есть ли AudioSource
        musicSource = GetComponent<AudioSource>();
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
        }

        sfxSource = GetComponent<AudioSource>();
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }

        musicSource.clip = musicClip;
        musicSource.loop = true;   // Loop музыки
        musicSource.playOnAwake = false;
        musicSource.volume = volume; // громкость 
        musicSource.Play(); // старт музыки


        sfxSource.loop = false;   // Loop музыки
        sfxSource.playOnAwake = false;
        sfxSource.volume = volume + 0.1f; // громкость 
        sfxSource.ignoreListenerPause = true;
    }
    public void PlayJump()
    {
        sfxSource.PlayOneShot(jumpSound);
    }

    public void PlayHit()
    {
        sfxSource.PlayOneShot(hitSound);
    }


    public void PlayEnter()
    {
        sfxSource.PlayOneShot(enterSound);
    }
    public void PlayPause()
    {

        sfxSource.PlayOneShot(pauseSound);
    }
    public void Update()
    {
        if (!isPaused)
            musicSource.UnPause();
        else
        {
            musicSource.Pause();
        }
    }

}
