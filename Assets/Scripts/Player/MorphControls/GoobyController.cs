using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoobyController : MasterController
{

    public Transform goobySprite;
    public Transform weapon;

    public Animator weaponAnim;

    private Camera cam;
    public GameObject swordCollider;
    bool isAttacking;
    float swordUpTimer;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        isAttacking = false;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerParent.stunned)
            MovePlayerStandard();

        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking && !playerParent.stunned)
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(mousePos.y, mousePos.x);
            StartCoroutine(EnableCollider(angle));
        }

        if (playerRb.velocity.x != 0 && !swordCollider.activeSelf)
        {
            goobySprite.localScale = new Vector3(initWidth * moveDirection, goobySprite.localScale.y, 1f);
        }

        
        if(swordUpTimer > 0)
        {
            swordUpTimer -= Time.deltaTime;
        }
        else if(swordUpTimer > -1)
        {
            weapon.transform.localScale = new Vector3(weapon.transform.localScale.x, 1f, 1f);
            swordUpTimer = -2;
        }
    }

    IEnumerator EnableCollider(float angle)
    {
        swordUpTimer = attackCooldown + 0.4f;
        isAttacking = true;
        if (angle < 0)
            angle += (2 * Mathf.PI);


        playerParent.hitDirection = new Vector2(goobySprite.localScale.x, 0);
        
        weaponAnim.Play("Swing");


        swordCollider.SetActive(true);
        yield return new WaitForSeconds(.1f);
        swordCollider.SetActive(false);
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;

        if(swordUpTimer > 0)
        {
            weapon.transform.localScale = new Vector3(weapon.transform.localScale.x, -weapon.transform.localScale.y, 1f);
        }
    }
}
