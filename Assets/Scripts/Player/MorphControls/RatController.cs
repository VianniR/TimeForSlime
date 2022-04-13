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

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        //animController = new AnimationController(blobAnim, "Idle");
        currSpeed = speed;
        canAttack = true;
        scratchFX.thisTransform = playerParent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (!playerParent.stunned && !isAttacking)
        {
            playerRb.velocity = new Vector2(horizontal * currSpeed, playerRb.velocity.y);
            
            if (horizontal == 0)
            {
                ratAnim.PlayAnim("Idle", 3);
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
        }
        else if (playerParent.stunned)
        {
            ratAnim.PlayAnim("Hurt", 3);
        }


        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            ratAnim.PlayAnim("RatAttack", 4);
            StartCoroutine(EnableAttackCollider());
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
