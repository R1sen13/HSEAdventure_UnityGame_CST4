using System.Xml.Serialization;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloudPrefab_1;
    public GameObject cloudPrefab_2;
    private GameObject chosen_prefab;

    public float spawnInterval = 15f;

    private float timer = 0f;

    void Start()
    {
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            // верхняя граница камеры
            float top = Camera.main.orthographicSize + Camera.main.transform.position.y;

            // чуть ниже верхнего края
            Vector3 pos = new Vector3(Camera.main.transform.position.x + 5.2f, top - 2f + Random.Range(-1.5f, 1.4f), 0f);

            // выбор облака
            int choice = Random.Range(1, 3);
            switch (choice)
            {
                case 1:
                    chosen_prefab = cloudPrefab_1;
                    break;
                case 2:
                    chosen_prefab = cloudPrefab_2;
                    break;
            }

            GameObject cloud = Instantiate(chosen_prefab, pos, Quaternion.identity);

            // делаем облако дочерним
            cloud.transform.parent = Camera.main.transform;

            timer = 0f; // сброс
        }
    }
}