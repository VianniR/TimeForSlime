using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobController : MasterController
{
    private bool attacking;
    private float currSpeed;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        playerParent = transform.parent.GetComponent<PlayerController>();
        //animController = new AnimationController(blobAnim, "Idle");
        currSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        playerRb.velocity = new Vector2(horizontal * currSpeed, playerRb.velocity.y);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currSpeed = speed * 1.5f;
        }
        else
        {
            currSpeed = speed;
        }
    }
}
