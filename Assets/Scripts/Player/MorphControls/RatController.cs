using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MasterController
{
    public GameObject attackCollider;
    bool canAttack;
    private float currSpeed;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        //animController = new AnimationController(blobAnim, "Idle");
        currSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if(!playerParent.stunned && canAttack)
            playerRb.velocity = new Vector2(horizontal * currSpeed, playerRb.velocity.y);
        else if(!playerParent.stunned)
        {
            playerRb.velocity = Vector2.zero;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currSpeed = speed * 1.5f;
        }
        else
        {
            currSpeed = speed;
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(EnableAttackCollider());
        }
    }

    IEnumerator EnableAttackCollider()
    {
        canAttack = false;

        playerParent.hitDirection = new Vector2(transform.localScale.x, 0);

        //weaponAnim.Play("Swing");

        attackCollider.SetActive(true);
        yield return new WaitForSeconds(.1f);
        attackCollider.SetActive(false);
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
