using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public float spawnCD;
    public GameObject[] pipeVariants;

    private float startSpawnCD;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startSpawnCD = spawnCD;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnCD <=0){
            int count = Random.Range(1,3);
            var h = Random.Range(750,1100);
            while(count >=1){
                if (h <= 950){
                    h = Random.Range(850,1150);
                }
                else{
                    h = Random.Range(600,850);
                }
                var x = Random.Range(-225,225);
                Vector3 pipeTransform = new Vector3(transform.position.x+x, h, transform.position.z);
                var randomVariant = Random.Range(0,pipeVariants.Length);
                Instantiate(pipeVariants[randomVariant],pipeTransform, Quaternion.identity);
                count--;

            }
            spawnCD = startSpawnCD;
        }
        else{
            spawnCD -= Time.deltaTime;
        }
    }
}
