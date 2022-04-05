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

    public GameObject currSnakeTile;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
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
            CreateBend(direction, Vector2.right);
            direction = Vector2.right;
            transform.position = CellToRealWorld(tilemapPos);
        }
        else if (xInput == -1 && !direction.Equals(Vector2.left))
        {
            CreateBend(direction, Vector2.left);
            direction = Vector2.left;
            transform.position = CellToRealWorld(tilemapPos);
        }
        else if (yInput == 1 && !direction.Equals(Vector2.up))
        {
            CreateBend(direction, Vector2.up);
            direction = Vector2.up;
            transform.position = CellToRealWorld(tilemapPos);
        }
        else if (yInput == -1 && !direction.Equals(Vector2.down))
        {
            CreateBend(direction, Vector2.down);
            direction = Vector2.down;
            transform.position = CellToRealWorld(tilemapPos);
        }
        if(timer < 0)
            rb.velocity = speed * direction;

        if (!prevTilemapPos.Equals(tilemapPos))
        {
            float cosTheta = (direction.x + 1);
            float sinTheta = (direction.y + direction.x * (direction.x - 1));
            currSnakeTile = Instantiate(snake, CellToRealWorld(tilemapPos), new Quaternion(0, 0, sinTheta, cosTheta));
        }

        if (xInput != 0 || yInput != 0)
            timer = -2;
        prevTilemapPos = tilemapPos;
    }

    public void CreateBend(Vector2 prevDir, Vector2 newDir)
    {
        float side = (Vector3.Cross(prevDir, newDir)).z;
        float cosTheta = (prevDir.x + 1);
        float sinTheta = (prevDir.y + prevDir.x * (prevDir.x - 1));
        GameObject newBend = Instantiate(snake, currSnakeTile.transform.position, new Quaternion(0, 0, sinTheta, cosTheta));
        newBend.transform.localScale = new Vector3(-1, side, 1) * 0.5f;
        newBend.GetComponent<Animator>().Play("Bend");
        Destroy(currSnakeTile);
        currSnakeTile = newBend;
    }

    public void SpawnSnake(Transform entrance)
    {
        gameObject.SetActive(true);
        lastEntrance = player.transform.position;
        player.SetActive(false);
        transform.position = entrance.position;
        Instantiate(snake, entrance.position, snake.transform.rotation);
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
            gameObject.SetActive(false);
            player.SetActive(true);
        }
    }
    float timer = -2;
    public void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (timer < 0)
                timer = 1f;
        }
    }

    private void FixedUpdate()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if(timer > -1)
        {
            gameObject.SetActive(false);
            player.SetActive(true);
            player.transform.position = lastEntrance;
            timer = -2;
        }
    }
}
