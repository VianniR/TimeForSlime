using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MasterEnemy
{
    [Header("Snake Specific")]
    bool isAttacking;
    public GameObject poisonProjectile;
    public float runSpeed;
    public float projectileSpeed;
    public Transform poisonPos;
    private AnimationController snakeAnim;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        gameObject.layer = 0;
        snakeAnim = GetComponent<AnimationController>();
    }

    float distFromPlayer;

    // Update is called once per frame
    void Update()
    {
        if (!angered)
        {
            IdleWalk();
            snakeAnim.PlayAnim("Walk", 1);
        }
        else if(!isAttacking && !stunned)
        {
            distFromPlayer = player.position.x - transform.position.x;

            if (Mathf.Abs(distFromPlayer) < 5 || Mathf.Abs(distFromPlayer) > 6)
            {
                direction = (int) Mathf.Sign((Mathf.Abs(distFromPlayer) - 6) * distFromPlayer);
                rb.velocity = new Vector2(direction * runSpeed, rb.velocity.y);
                transform.localScale = new Vector3(direction * initWidth, transform.localScale.y, 1f);
                snakeAnim.PlayAnim("Walk", 1);
            }
            else
            {
                snakeAnim.PlayAnim("Idle", 2);
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
        direction = (int)Mathf.Sign(distFromPlayer);
        transform.localScale = new Vector3(direction * initWidth, transform.localScale.y, 1f);
        snakeAnim.PlayAnim("Attack", 3);
        canAttack = false;
        isAttacking = true;
        yield return new WaitForSeconds(0.15f);
        GameObject proj = Instantiate(poisonProjectile, poisonPos.position, poisonProjectile.transform.rotation);
        proj.GetComponent<KnockbackData>().targetTransform = (transform);

        float dist = player.transform.position.x - poisonPos.position.x;
        proj.GetComponent<Rigidbody2D>().velocity = new Vector2(dist * 0.8f, 6f);


        yield return new WaitForSeconds(0.25f);
        isAttacking = false;
        StartCoroutine(AttackCooldown());
    }
}
