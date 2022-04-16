using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterController : MonoBehaviour
{
    protected Rigidbody2D playerRb;
    public float speed;
    public float jumpForce;
    public bool onGround;
    public PlayerController playerParent;
    public float morphSize;

    protected bool onSlime = false;
    protected string currCollision = "";
    protected const string NORMAL_GROUND = "NormalGround";
    protected const string SLIME_GROUND = "slimeGround";
    protected const string SPIKES = "spike";
    protected const string ICE = "Ice";

    public Transform groundCheckers;
    protected GroundCheck collisionCheckScript;

    public Animator morphAnim;
    public float fallFactor;
    protected AnimationController animController;

    private bool isJumping;
    public float gravity;
    public SpriteRenderer playerSprite;

    public float attackCooldown;
    protected float initWidth;

    protected bool isAttacking;

    // Start is called before the first frame update
    public void Start()
    {
        playerRb = transform.parent.GetComponent<Rigidbody2D>();
        collisionCheckScript = groundCheckers.gameObject.GetComponent<GroundCheck>();
        initWidth = transform.localScale.x;
        playerParent = transform.parent.GetComponent<PlayerController>();
        animController = GetComponent<AnimationController>();
        isAttacking = false;
    }

    public void MovePlayerStandard()
    {
        if (onGround)
            currCollision = CheckCollisions();

        //Player Movement
        float horizontal = Input.GetAxisRaw("Horizontal");

        if(!onGround)
        {
            if (playerRb.velocity.x == horizontal * speed)
            {
                horizontal = Input.GetAxisRaw("Horizontal");
            }
            else
            {
                horizontal = Input.GetAxis("Horizontal") * 2;
            }
        }

            playerRb.velocity = new Vector2(Mathf.Clamp(horizontal * speed, -speed, speed), playerRb.velocity.y);

        //Jumpingd
        if (isJumping)
        {
            if (playerRb.velocity.y >= 5)
            {
                playerRb.gravityScale = gravity;
            }
            else if (playerRb.velocity.y < 2 && playerRb.velocity.y > 0)
            {
                playerRb.gravityScale = gravity / 5f;
            }
            else
            {
                playerRb.gravityScale = gravity * 1.2f;
            }
        }

        if (horizontal != 0 && !isAttacking && !playerParent.stunned)
        {
            playerParent.SetDirection((int)Mathf.Sign(horizontal));
        }


        //Movement
        if (!Mathf.Approximately(playerRb.velocity.x, 0.0f))
        {
            animController.PlayAnim("Run", 2);
        }

        if (Input.GetKey(KeyCode.Space) && onGround && !isJumping)
        {
            onGround = false;
            isJumping = true;
            playerRb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        currCollision = CheckCollisions();
        if (collision.gameObject.CompareTag("RealGroundObject"))
        {
            ResetToground();
            onSlime = true;
        }
        else if (collision.gameObject.CompareTag("GroundObject"))
        {
            ResetToground();
        }
        //Debug.Log(currCollision);
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        if (!onGround)
        {
            currCollision = CheckCollisions();
        }
        if (collision.gameObject.CompareTag("RealGroundObject"))
        {
            onSlime = true;
            ResetToground();
        }
        else if (collision.gameObject.CompareTag("GroundObject"))
        {
            ResetToground();
        }
    }

    protected string CheckCollisions()
    {
        string ceilingCheck = collisionCheckScript.CeilingCollision();
        string groundCheck = collisionCheckScript.GroundCollision();
        if (!groundCheck.Equals(""))
        {
            ResetToground();
        }
  
        if(!ceilingCheck.Equals(""))
        {
            isJumping = false;
            playerRb.gravityScale = gravity;
        }
        string leftWallCheck = collisionCheckScript.LeftWallCollision();
        string rightWallCheck = collisionCheckScript.RightWallCollision();


        string anyCollision = ceilingCheck + leftWallCheck + rightWallCheck;

        onSlime = anyCollision.Contains("slime");

        return anyCollision;
    }

    public int normalizeFloat(float value)
    {
        if (value < 0)
            return -1;
        if (value > 0)
            return 1;
        return 0;
    }

    public void ResetToground()
    {
        onGround = true;
        isJumping = false;
        playerRb.gravityScale = gravity;
    }

    [SerializeField]
    private Color invincColor;

    public IEnumerator Invincible()
    {
        gameObject.layer = 12;
        playerSprite.color = invincColor;
        yield return new WaitForSeconds(0.1f);
        playerSprite.color = new Color(255, 255, 255, 1);
        yield return new WaitForSeconds(0.15f);
        playerSprite.color = invincColor;
        yield return new WaitForSeconds(0.1f);
        playerSprite.color = new Color(255, 255, 255, 1);
        yield return new WaitForSeconds(0.15f);
        playerSprite.color = invincColor;
        yield return new WaitForSeconds(0.1f);
        playerSprite.color = new Color(255, 255, 255, 1);
        yield return new WaitForSeconds(0.15f);
        playerSprite.color = invincColor;
        yield return new WaitForSeconds(0.1f);
        playerSprite.color = new Color(255, 255, 255, 1);
        yield return new WaitForSeconds(0.3f);
        gameObject.layer = 9;
    }

    public void MoveRb(float x)
    {
        bool onWall = collisionCheckScript.LeftWallCollision().Equals("") && collisionCheckScript.RightWallCollision().Equals("");
        if (Mathf.Approximately(playerRb.velocity.x, 0) && onWall)
            playerRb.position += new Vector2(x * playerParent.moveDirection, 0);
    }
}