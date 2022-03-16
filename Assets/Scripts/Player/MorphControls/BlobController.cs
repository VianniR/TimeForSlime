using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    public float speed;
    public bool onGround;
    private PlayerController playerParent;

    private bool onSlime = false;
    private string currCollision = "";
    private const string NORMAL_GROUND = "NormalGround";
    private const string SLIME_GROUND = "slimeGround";
    private const string SPIKES = "spike";
    private const string ICE = "Ice";

    public Transform groundCheckers;
    private GroundCheck collisionCheckScript;

    //public Animator blobAnim;
    public AnimationController animController;

    private bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        playerParent = transform.parent.GetComponent<PlayerController>();
        playerRb = transform.parent.GetComponent<Rigidbody2D>();
        collisionCheckScript = groundCheckers.gameObject.GetComponent<GroundCheck>();
        //animController = new AnimationController(blobAnim, "Idle");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        playerRb.velocity = new Vector2(horizontal * speed, playerRb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currCollision = CheckCollisions();
        if (collision.gameObject.CompareTag("RealGroundObject"))
        {
            onGround = true;
            onSlime = true;
        }
        else if (collision.gameObject.CompareTag("GroundObject"))
        {
            onGround = true;
        }
        //Debug.Log(currCollision);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!onGround)
        {
            currCollision = CheckCollisions();
        }
        if (collision.gameObject.CompareTag("RealGroundObject"))
        {
            onGround = true;
            onSlime = true;
        }
        else if (collision.gameObject.CompareTag("GroundObject"))
        {
            onGround = true;
        }
    }

    string CheckCollisions()
    {
        if (!collisionCheckScript.GroundCollision().Equals(""))
        {
            onGround = true;
        }
        string ceilingCheck = collisionCheckScript.CeilingCollision();
        string leftWallCheck = collisionCheckScript.LeftWallCollision();
        string rightWallCheck = collisionCheckScript.RightWallCollision();


        string anyCollision = ceilingCheck + leftWallCheck + rightWallCheck;

        onSlime = anyCollision.Contains("slime");

        return anyCollision;
    }
}
