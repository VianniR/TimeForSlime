using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MasterEnemy
{
    [Header("Enemy Specific")]
    bool isAttacking;
    public GameObject poisonProjectile;
    public float runSpeed;
    public float projectileSpeed;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        gameObject.layer = 0;
    }

    float distFromPlayer;

    // Update is called once per frame
    void Update()
    {
        if (!angered)
        {
            IdleWalk();
        }
        else if(!isAttacking)
        {
            distFromPlayer = player.position.x - transform.position.x;

            if (Mathf.Abs(distFromPlayer) < 5 || Mathf.Abs(distFromPlayer) > 6)
            {
                float moveDir = Mathf.Sign((Mathf.Abs(distFromPlayer) - 6) * distFromPlayer);
                rb.velocity = new Vector2(moveDir * runSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
            
            if(canAttack)
            {
                StartCoroutine(SpitPoison());
            }
        }

        if (!SeekPlayerRay() && !unanger.Running)
        {
            unanger = new Task(CannotSeePlayer());
        }
        else if (SeekPlayerRay() && unanger.Running)
        {
            unanger.Stop();
        }
        else if(SeekPlayerRay() && !angered)
        {
            StartCoroutine(AttackCooldown());
            angered = true;
        }
    }

    IEnumerator SpitPoison()
    {
        canAttack = false;
        isAttacking = true;
        Rigidbody2D projRb = Instantiate(poisonProjectile, transform.position, poisonProjectile.transform.rotation).GetComponent<Rigidbody2D>();
        projRb.velocity = new Vector2(Mathf.Sign(distFromPlayer) * projectileSpeed, 5f);
        yield return new WaitForSeconds(0.3f);
        isAttacking = false;
        StartCoroutine(AttackCooldown());
    }
}
