using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MasterController
{
    // Start is called before the first frame update
    bool canAttack;
    public GameObject poisonProjectile;
    public float projectileSpeed;
    // Update is called once per frame
    new void Start()
    {
        base.Start();
        canAttack = true;
    }
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        playerRb.velocity = new Vector2(horizontal * speed, playerRb.velocity.y);
        if (playerRb.velocity.x != 0)
        {
            playerParent.SetDirection((int)Mathf.Sign(playerRb.velocity.x));
        }

        if (canAttack && Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(SpitPoison());
        }
    }
    IEnumerator SpitPoison()
    {
        canAttack = false;
        GameObject proj = Instantiate(poisonProjectile, transform.position, poisonProjectile.transform.rotation);
        proj.tag = "PlayerWeapon";
        proj.layer = 8;
        proj.GetComponent<Rigidbody2D>().velocity = new Vector2(playerParent.moveDirection * projectileSpeed, 5f);
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
