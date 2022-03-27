using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoobyController : MasterController
{
    public Transform weapon;

    public Animator weaponAnim;

    private Camera cam;
    public GameObject swordCollider;
    float swordUpTimer;
    int swordDir;

    public AnimationController weaponAnimContr;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        swordDir = 0;
    }

    // Update is called once per frame
    void Update()
    {
        morphAnim.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        if (!playerParent.stunned)
            MovePlayerStandard();

        if (Input.GetKey(KeyCode.Mouse0) && !isAttacking && !playerParent.stunned)
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(mousePos.y, mousePos.x);
            StartCoroutine(EnableCollider(angle));
        }

        
        if(swordUpTimer > 0)
        {
            swordUpTimer -= Time.deltaTime;
        }
        else if(swordUpTimer > -1)
        {
            swordDir = 0;
            swordUpTimer = -2;
        }
    }

    IEnumerator EnableCollider(float angle)
    {
        swordUpTimer = 0.75f;
        isAttacking = true;
        if (angle < 0)
            angle += (2 * Mathf.PI);


        playerParent.hitDirection = new Vector2(playerParent.moveDirection, 0);

        if (swordDir == 0)
        {
            animController.PlayAnim("SwordDown", 2);
            weaponAnimContr.PlayAnim("SwordDown", 2);
        }
        else if (swordDir == 1)
        {
            animController.PlayAnim("SwordUp", 2);
            weaponAnimContr.PlayAnim("SwordUp", 2);
        }
        else
        {
            animController.PlayAnim("SwordThrust", 4);
        }

        yield return new WaitForSeconds(.1f);
        swordCollider.SetActive(true);
        yield return new WaitForSeconds(.1f);
        swordCollider.SetActive(false);
        yield return new WaitForSeconds(attackCooldown);

        swordDir++;
        if (swordDir > 2)
            swordDir = 0;

        isAttacking = false;
    }
}
