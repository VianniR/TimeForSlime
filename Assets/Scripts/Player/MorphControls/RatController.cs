using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MasterController
{
    public GameObject attackCollider;
    bool canAttack;
    private float currSpeed;
    public AnimationController ratAnim;
    public HitFX scratchFX;
    private bool jumping;
    private int wallDir;

    [Header("Wall Run")]
    public float maxWallRunSpeed;
    public float deceleration;
    private float wallRunSpeed;

    private float ogScale;


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        //animController = new AnimationController(blobAnim, "Idle");
        currSpeed = speed;
        canAttack = true;
        scratchFX.thisTransform = playerParent.transform;
        jumping = false;
        wallRunSpeed = maxWallRunSpeed;
        playerRb.mass = 0.01f;
        ogScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (onGround)
        {
            wallRunSpeed = maxWallRunSpeed;
        }

        onGround = !collisionCheckScript.GroundCollision().Equals("");
        wallDir = 0;
        if (!collisionCheckScript.LeftWallCollision().Equals(""))
        {
            wallDir = -1;
        }
        if (!collisionCheckScript.RightWallCollision().Equals(""))
        {
            wallDir = 1;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        if (!playerParent.stunned && !isAttacking)
        {
            transform.localScale = new Vector3(ogScale, ogScale, 1);
            if (wallDir == 0)
            {
                ratAnim.getAnimator().SetBool("WallRun", false);
                if (horizontal == 0)
                {
                    ratAnim.PlayAnim("Idle", 4);
                }
                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    currSpeed = speed * 1.5f;
                    ratAnim.getAnimator().SetBool("Angry", true);
                    ratAnim.PlayAnim("Run", 1);
                    playerParent.SetDirection((int)Mathf.Sign(horizontal));
                }
                else
                {
                    currSpeed = speed;
                    ratAnim.getAnimator().SetBool("Angry", false);
                    ratAnim.PlayAnim("Walk", 2);
                    playerParent.SetDirection((int)Mathf.Sign(horizontal));
                }
                if (!jumping)
                    playerRb.velocity = new Vector2(horizontal * currSpeed, playerRb.velocity.y);

                if (Input.GetKeyDown(KeyCode.Space) && onGround)
                {
                    onGround = false;
                    jumping = true;
                    playerRb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                }
            }
            else
            {
                wallRunSpeed -= Time.deltaTime * deceleration;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    playerRb.velocity = new Vector2(-wallDir * 10, 15);
                    jumping = true;
                    transform.localScale = new Vector3(transform.localScale.x, ogScale, transform.localScale.z);
                }

                if (!jumping)
                {
                    transform.localScale = new Vector3(ogScale, ogScale * Mathf.Sign(playerRb.velocity.y), transform.localScale.z);
                    playerParent.SetDirection(wallDir);
                }

                if (horizontal != wallDir && onGround && !jumping)
                {
                    playerRb.velocity = new Vector2(horizontal * currSpeed, playerRb.velocity.y);
                    transform.localScale = new Vector3(ogScale, ogScale, transform.localScale.z);
                }
                else if (!jumping && wallRunSpeed > 0)
                {
                    if (horizontal == wallDir)
                    {
                        playerRb.velocity = new Vector2(0, horizontal * wallRunSpeed * wallDir);
                    }
                    else
                    {
                        playerRb.velocity = new Vector2(0, -speed);
                    }
                    ratAnim.PlayAnim("WallRun", 3);
                    ratAnim.getAnimator().SetBool("WallRun", true);
                }

                if (onGround)
                {
                    ratAnim.getAnimator().SetBool("WallRun", false);
                    ratAnim.PlayAnim("Idle", 4);
                    transform.localScale = new Vector3(ogScale, ogScale, transform.localScale.z);
                }
            }
        }
        else if (playerParent.stunned)
        {
            ratAnim.PlayAnim("Hurt", 4);
        }


        if (Input.GetKeyDown(KeyCode.Mouse0) && wallDir == 0)
        {
            ratAnim.PlayAnim("RatAttack", 4);
            StartCoroutine(EnableAttackCollider());
        }

        if (jumping && onGround)
        {
            jumping = false;
        }
    }

    IEnumerator EnableAttackCollider()
    {
        canAttack = false;
        isAttacking = true;

        playerParent.hitDirection = new Vector2(playerParent.moveDirection, 0);

        yield return new WaitForSeconds(.25f);
        attackCollider.SetActive(true);
        yield return new WaitForSeconds(.1f);
        attackCollider.SetActive(false);
        isAttacking = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
