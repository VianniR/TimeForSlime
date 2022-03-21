using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterEnemy : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField]
    public int direction;
    public float idleSpeed;
    public GroundCheck groundCheck;
    public float stunTime;
    protected bool stunned;
    protected bool onGround;
    protected Transform player;
    public float sightRadius;
    public LayerMask sightLayers;
    protected EnemyHealth healthScrpt;

    //Anger & attacks
    [Header("Attacks")]
    public bool angered;
    protected Task unanger;
    protected bool canAttack;
    public float attackCooldown;

    private float initWidth;


    // Start is called before the first frame update
    protected void Start()
    {
        angered = false;
        healthScrpt = gameObject.GetComponent<EnemyHealth>();
        canAttack = false;
        unanger = new Task(CannotSeePlayer());
        onGround = true;
        direction = 1;
        player = GameObject.Find("Player").transform;
        initWidth = transform.localScale.x;
    }

    public void IdleWalk()
    {
        string wall;
        if (!stunned && onGround)
            rb.velocity = new Vector2(idleSpeed * direction, rb.velocity.y);

        if (direction == 1)
        {
            wall = groundCheck.RightWallCollision();
        }
        else
        {
            wall = groundCheck.LeftWallCollision();
        }
        onGround = groundCheck.GroundCollision().Contains("Ground");
        //Debug.Log(wall);
        if (wall.Contains("Ground"))
        {
            direction *= -1;
            transform.localScale = new Vector3(direction * initWidth, transform.localScale.y, 1f);
        }
    }

    public void SetDirection(int newDir)
    {
        direction = newDir;
        transform.localScale = new Vector3(direction * initWidth, transform.localScale.y, 1f);
    }

    public bool SeekPlayerRay()
    {
        Vector3 lookDir = player.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDir, sightRadius, sightLayers);
        Debug.DrawLine(transform.position, transform.position + lookDir.normalized * sightRadius);
        //Debug.Log(hit.collider.gameObject.tag);
        return hit.collider != null && (hit.collider.gameObject.CompareTag("Player"));
    }

    public IEnumerator StunEnemy()
    {
        stunned = true;
        yield return new WaitForSeconds(stunTime);
        stunned = false;
    }

    public IEnumerator CannotSeePlayer()
    {
        yield return new WaitForSeconds(2f);
        angered = false;
        canAttack = false;
        //defaultKnockback.SetActive(false);
        gameObject.layer = 8;
    }
    public IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
