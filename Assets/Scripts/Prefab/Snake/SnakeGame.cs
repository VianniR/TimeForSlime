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

    Vector3 lastEntrance = new Vector2(0, 0);

    public List<GameObject> snakeTiles;

    float snakeLength = -2;
    bool hitWall;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        snakeTiles = new List<GameObject>();
        hitWall = false;
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        player.transform.position = transform.position;
        if (xInput == 1 && !direction.Equals(Vector2.right))
        {
            CreateBend(Vector2.right);
            direction = Vector2.right;
            transform.position = CellToRealWorld(tilemapPos);
        }
        else if (xInput == -1 && !direction.Equals(Vector2.left))
        {
            CreateBend(Vector2.left);
            direction = Vector2.left;
            transform.position = CellToRealWorld(tilemapPos);
        }
        else if (yInput == 1 && !direction.Equals(Vector2.up))
        {
            CreateBend(Vector2.up);
            direction = Vector2.up;
            transform.position = CellToRealWorld(tilemapPos);
        }
        else if (yInput == -1 && !direction.Equals(Vector2.down))
        {
            CreateBend(Vector2.down);
            direction = Vector2.down;
            transform.position = CellToRealWorld(tilemapPos);
        }
        if(snakeLength > 0)
            rb.velocity = speed * direction;

        tilemapPos = grid.WorldToCell(transform.position);
        if (!prevTilemapPos.Equals(tilemapPos))
        {
            GameObject newSnake = SpawnNewTail(CellToRealWorld(tilemapPos), false, direction);
            snakeTiles.Add(newSnake);
        }

        prevTilemapPos = tilemapPos;
    }

    public void CreateBend(Vector2 newDir)
    {
        GameObject newBend;
        Vector2 prevDir = snakeTiles[snakeTiles.Count - 2].GetComponent<SnakeTail>().lookDirection;
        float turnDir = (Vector3.Cross(prevDir, newDir)).z;
        if (turnDir != 0)
        {
            newBend = SpawnNewTail(snakeTiles[snakeTiles.Count - 1].transform.position, true, prevDir);
            newBend.transform.localScale = new Vector3(-1, turnDir, 1) * 0.5f;
            newBend.GetComponent<Animator>().Play("Bend");
        }
        else
        {
            newBend = SpawnNewTail(snakeTiles[snakeTiles.Count - 1].transform.position, false, newDir);
        }
        Destroy(snakeTiles[snakeTiles.Count - 1]);
        snakeTiles.Remove(snakeTiles[snakeTiles.Count - 1]);
        snakeTiles.Add(newBend);
    }

    public GameObject SpawnNewTail(Vector3 position, bool bend, Vector2 tailDirection)
    {
        float cosTheta = (tailDirection.x + 1);
        float sinTheta = (tailDirection.y + tailDirection.x * (tailDirection.x - 1));
        GameObject newTail = Instantiate(snake, CellToRealWorld(tilemapPos), new Quaternion(0, 0, sinTheta, cosTheta));
        newTail.GetComponent<SnakeTail>().SetVars(this, snakeLength, bend, tailDirection);
        return newTail;
    }

    public void SpawnSnake(Transform entrance)
    {
        gameObject.SetActive(true);
        lastEntrance = player.transform.position;
        player.SetActive(false);
        transform.position = entrance.position;
        snakeTiles.Add(Instantiate(snake, entrance.position, snake.transform.rotation));
    }

    public void SetDirection(float angle)
    {
        direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        transform.position = CellToRealWorld(tilemapPos);
    }

    Vector3 CellToRealWorld(Vector3Int pos)
    {
        return grid.CellToWorld(pos) + new Vector3(0.25f, 0.25f, 0);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("SnakeExit"))
        {
            player.SetActive(true);
            snakeLength = 0.2f;
            foreach (GameObject s in snakeTiles)
                Destroy(s);
            snakeTiles.Clear();
            gameObject.SetActive(false);
        }
    }
    public void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            hitWall = true;
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            hitWall = false;
        }
    }

    private void FixedUpdate()
    {
        if(hitWall)
        {
            snakeLength -= Time.deltaTime;
        }
        if(snakeLength < 0.1f && snakeLength > -1)
        {
            player.SetActive(true);
            player.transform.position = lastEntrance;
            snakeLength = 0.2f;
            snakeTiles.Clear();
            gameObject.SetActive(false);
        }
    }

    public void SetTailLength(float length)
    {
        snakeLength = length;
    }
}
