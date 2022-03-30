using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeGame : MonoBehaviour
{
    private GameObject player;
    public GameObject snake;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnSnake(Transform entrance)
    {
        snake.SetActive(true);
        snake.transform.position = entrance.position;
    }

}
