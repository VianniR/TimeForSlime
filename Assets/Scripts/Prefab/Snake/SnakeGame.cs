using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeGame : MonoBehaviour
{
    private GameObject player;
    Vector2 direction;
    public float speed;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        direction = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        player.transform.position = transform.position;
        if (xInput == 1)
        {
            direction = Vector2.right;
        }
        else if (xInput == -1)
        {
            direction = Vector2.left;
        }
        else if (yInput == 1)
        {
            direction = Vector2.up;
        }
        else if (yInput == -1)
        {
            direction = Vector2.down;
        }

        rb.velocity = speed * direction;
    }

    public void SpawnSnake(Transform entrance)
    {
        player.SetActive(false);
        gameObject.SetActive(true);
        transform.position = entrance.position;
    }

}
