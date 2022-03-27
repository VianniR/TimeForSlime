using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRat : MasterEnemy
{
    [Header("Enemy Specific")]
    public float runSpeed;
    public GameObject attackRange;
    public GameObject defaultKnockback;
    public AnimationController ratAnim;

    bool isAttacking;

    private void Start()
    {
        base.Start();
        gameObject.layer = 8;
        canAttack = false;
        ratAnim.PlayAnim("Walk", 1);
        isAttacking = false;
    }
    // Update is called once per frame
    void Update()
    {
        ratAnim.getAnimator().SetBool("Angry", angered);
        if (!angered)
        {
            IdleWalk();
        }
        else
        {
            float distFromPlayer = player.position.x - transform.position.x;

            if (!isAttacking)
            {
                SetDirection((int)Mathf.Sign(distFromPlayer));
            }

            if (Mathf.Abs(distFromPlayer) > 2f && !stunned && !isAttacking)
            {
                rb.velocity = new Vector2(runSpeed * direction, rb.velocity.y);
                ratAnim.PlayAnim("Run", 1);
            }
            else if (canAttack && !stunned)
            {
                StartCoroutine(Slash());
                ratAnim.PlayAnim("RatAttack", 4);
            }
            else if(stunned)
            {
                ratAnim.PlayAnim("Hurt", 3);
            }
            else
            {
                ratAnim.PlayAnim("IdleAngry", 2);
            }

            if (!SeekPlayerRay() && !unanger.Running)
            {
                unanger = new Task(CannotSeePlayer());
            }
            else if(SeekPlayerRay() && unanger.Running)
            {
                unanger.Stop();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!angered && collision.gameObject.CompareTag("PlayerWeapon"))
        {
            angered = true;
            StartCoroutine(AttackCooldown());
            gameObject.layer = 0;
        }
    }

    IEnumerator Slash()
    {
        canAttack = false;
        isAttacking = true;
        yield return new WaitForSeconds(0.3f);
        attackRange.SetActive(true);
        transform.Translate(0.03f * direction, 0, 0);
        yield return new WaitForSeconds(.1f);
        attackRange.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        isAttacking = false;
        StartCoroutine(AttackCooldown());
    }
}
