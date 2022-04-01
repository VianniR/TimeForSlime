using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SnakeGame : MonoBehaviour
{
    private GameObject player;
    Vector2 direction;
    public float speed;
    private Rigidbody2D rb;
    public GridLayout grid;
    public Tilemap tilemap;
    public GameObject snake;

    Vector3Int tilemapPos;
    Vector3Int prevTilemapPos;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
        direction = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        tilemapPos = grid.WorldToCell(transform.position);
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        player.transform.position = transform.position;
        if (xInput == 1 && !direction.Equals(Vector2.right))
        {
            direction = Vector2.right;
            transform.position = CellToRealWorld(tilemapPos);
        }
        else if (xInput == -1 && !direction.Equals(Vector2.left))
        {
            direction = Vector2.left;
            transform.position = CellToRealWorld(tilemapPos);
        }
        else if (yInput == 1 && !direction.Equals(Vector2.up))
        {
            direction = Vector2.up;
            transform.position = CellToRealWorld(tilemapPos);
        }
        else if (yInput == -1 && !direction.Equals(Vector2.down))
        {
            direction = Vector2.down;
            transform.position = CellToRealWorld(tilemapPos);
        }

        rb.velocity = speed * direction;
        if(!prevTilemapPos.Equals(tilemapPos))
        {
            Instantiate(snake, CellToRealWorld(tilemapPos), snake.transform.rotation);
        }
        prevTilemapPos = tilemapPos;
    }

    public void SpawnSnake(Transform entrance)
    {
        gameObject.SetActive(true);
        player.SetActive(false);
        transform.position = entrance.position;
        Instantiate(snake, entrance.position, new Quaternion(direction.x, direction.y, 0, 0));
    }

    Vector3 CellToRealWorld(Vector3Int pos)
    {
        return grid.CellToWorld(pos) + new Vector3(0.25f, 0.25f, 0);
    }
}
