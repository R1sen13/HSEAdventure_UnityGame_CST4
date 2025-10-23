using System.Xml.Serialization;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform startPlatform;
    public GameObject staticPlatformPrefab;
    public GameObject movingPlatformPrefab;
    public GameObject endingPlatformPrefab;
    public GameObject staticEnemy;
    public GameObject movingEnemy;
    public int platformAmount = 100;
    public float enemyChance = 0.2f;
    public float enemyMinDistanceX = 0f;
    public float enemyMaxDistanceX = 1f;
    public float enemyDistanceY = 0.5f;
    public float levelWidth = 3.7f;
    public float minY = 0.5f;
    public float maxY = 2f;
    void Start()
    {
        Vector3 spawnPosition = startPlatform.position;



        for (int i = 0; i < platformAmount; i++)
        {
            float choice = Random.Range(1, 101);
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            if (i == platformAmount - 1)
            {
                Vector3 final_pos = spawnPosition;
                final_pos.x = 0;
                Instantiate(endingPlatformPrefab, final_pos, Quaternion.identity);
                break;
            }
            if (choice >= 1 && choice <= 70) // Выпала статичная платформа
            {
                Instantiate(staticPlatformPrefab, spawnPosition, Quaternion.identity);
                if (Random.value < enemyChance)
                {
                    Vector3 enemy_spawn = spawnPosition;
                    float mid_pos = Random.Range(enemyMinDistanceX, enemyMaxDistanceX);
                    if (Random.value < 0.5f)
                        enemy_spawn.x += mid_pos;
                    else
                        enemy_spawn.x += -mid_pos;
                    enemy_spawn.y += enemyDistanceY;
                    Instantiate(staticEnemy, enemy_spawn, Quaternion.identity);

                }
            }
            else if (choice >= 71 && choice <= 101)
            { // Выпала двигающаяся платформа
                if (Random.value < enemyChance)
                {
                    Vector3 enemy_spawn = spawnPosition;
                    float mid_pos = Random.Range(enemyMinDistanceX, enemyMaxDistanceX);
                    enemy_spawn.x += Random.Range(-mid_pos, mid_pos);
                    enemy_spawn.y += enemyDistanceY;
                    Instantiate(movingEnemy, enemy_spawn, Quaternion.identity);
                }
                Instantiate(movingPlatformPrefab, spawnPosition, Quaternion.identity);
            }
        }


    }
}

