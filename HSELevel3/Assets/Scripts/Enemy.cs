using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int pos;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pos = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(pos,0,0) * (speed*Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("pl1")){
            pos = 1;
            gameObject.transform.Rotate(0, 180, 0);
        }
        else if(col.gameObject.CompareTag("pl2")){
            pos = -1;
            gameObject.transform.Rotate(0, 180, 0);
        }
    }
}
