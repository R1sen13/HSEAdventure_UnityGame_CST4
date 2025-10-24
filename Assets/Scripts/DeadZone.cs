using System;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public LogicManager logic;
    public MusicManager soundPlayer;
    void Start()
    {
        soundPlayer = GameObject.FindWithTag("MusicManager").GetComponent<MusicManager>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") == true)
        {
            soundPlayer.PlayHit();
            logic.isGameOver = true;

            //Game over script placeholder
        }
    }

}
