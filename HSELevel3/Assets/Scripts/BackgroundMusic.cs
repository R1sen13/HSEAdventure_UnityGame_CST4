using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip myClip;
    public AudioSource start, background;
    public static bool IsDeath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        start.Play();
        background.PlayDelayed(myClip.length);
    }
}
