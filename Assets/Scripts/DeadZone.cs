using System;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public LogicManager logic;
    void Start()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") == true)
        {
            logic.isGameOver = true;

            //Game over script placeholder
        }
    }

}
