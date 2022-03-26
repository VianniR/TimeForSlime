using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MasterController
{
    public GameObject attackCollider;
    bool canAttack;
    private float currSpeed;
    public AnimationController ratAnim;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        //animController = new AnimationController(blobAnim, "Idle");
        currSpeed = speed;
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (!playerParent.stunned && canAttack)
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
            }
            else
            {
                currSpeed = speed;
                ratAnim.getAnimator().SetBool("Angry", false);
                ratAnim.PlayAnim("Walk", 2);
            }
        }
        else if (!playerParent.stunned)
        {
            playerRb.velocity = Vector2.zero;
        }


        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(EnableAttackCollider());
        }

        if (playerRb.velocity.x != 0)
        {
            transform.localScale = new Vector3(initWidth * Mathf.Sign(playerRb.velocity.x), transform.localScale.y, 1f);
        }
    }

    IEnumerator EnableAttackCollider()
    {
        canAttack = false;

        playerParent.hitDirection = new Vector2(transform.localScale.x, 0);

        attackCollider.SetActive(true);
        yield return new WaitForSeconds(.25f);
        attackCollider.SetActive(false);
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
