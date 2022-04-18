using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MasterController
{
    // Start is called before the first frame update
    bool canAttack;
    public GameObject poisonProjectile;
    public float projectileSpeed;
    public Transform poisonPos;
    public AnimationController snakeAnim;
    // Update is called once per frame
    new void Start()
    {
        base.Start();
        canAttack = true;
    }
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if(!isAttacking)
            playerRb.velocity = new Vector2(horizontal * speed, playerRb.velocity.y);
        else
            playerRb.velocity = new Vector2(0, playerRb.velocity.y);


        if (playerRb.velocity.x != 0)
        {
            snakeAnim.PlayAnim("Walk", 1);
            playerParent.SetDirection((int)Mathf.Sign(playerRb.velocity.x));
        }
        else
        {
            snakeAnim.PlayAnim("Idle", 2);
        }

        if (canAttack && Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(SpitPoison());
        }
    }
    IEnumerator SpitPoison()
    {
        snakeAnim.PlayAnim("Attack", 3);
        canAttack = false;
        isAttacking = true;
        yield return new WaitForSeconds(0.15f);
        GameObject proj = Instantiate(poisonProjectile, poisonPos.position, poisonProjectile.transform.rotation);
        proj.tag = "PlayerWeapon";
        proj.layer = 8;
        proj.GetComponent<Rigidbody2D>().velocity = new Vector2(playerParent.moveDirection * projectileSpeed, 6f);
        proj.GetComponent<KnockbackData>().targetTransform = transform;
        yield return new WaitForSeconds(0.25f);
        isAttacking = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        
    }
}
