using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Clouds : MonoBehaviour
{
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left*speed*Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("deleteclouds")){
            Destroy(gameObject);
        }

    }
}
