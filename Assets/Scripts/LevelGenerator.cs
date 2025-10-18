using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform startPlatform;
    public GameObject platformPrefab;
    public int platformAmount = 100;
    public float levelWidth = 3.7f;
    public float minY = 0.5f;
    public float maxY = 2f;
    void Start()
    {
        Vector3 spawnPosition = startPlatform.position;


        for (int i = 0; i < platformAmount; i++)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
