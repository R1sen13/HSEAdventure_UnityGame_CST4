using UnityEngine;

public class BorderScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform player;
    public float leftTeleport = -4f;
    public float rightTeleport = 4.1f;
    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D player)
    {
        Vector2 new_pos = player.transform.position;
        if (player.CompareTag("Player") == true)
        {
            switch (gameObject.tag)
            {
                case "LeftBorder":
                    new_pos.x = rightTeleport;
                    player.transform.position = new_pos;
                    break;

                case "RightBorder":
                    new_pos.x = leftTeleport;
                    player.transform.position = new_pos;
                    break;


            }
        }


    }
}
