using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRat : MasterEnemy
{
    [Header("Enemy Specific")]
    public float runSpeed;
    public GameObject attackRange;
    public GameObject defaultKnockback;

    private void Start()
    {
        base.Start();
        gameObject.layer = 8;
        canAttack = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!angered)
        {
            IdleWalk();
        }
        else
        {
            float distFromPlayer = player.position.x - transform.position.x;

            if (Mathf.Abs(distFromPlayer) > 1f && !stunned && !attackRange.activeSelf)
            {
                SetDirection((int)Mathf.Sign(distFromPlayer));
                rb.velocity = new Vector2(runSpeed * direction, rb.velocity.y);
            }
            else if (canAttack)
            {
                StartCoroutine(Slash());
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
        }
    }

    IEnumerator Slash()
    {
        canAttack = false;
        attackRange.SetActive(true);
        transform.Translate(0.03f * direction, 0, 0);
        yield return new WaitForSeconds(0.2f);
        attackRange.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(AttackCooldown());
    }
}
