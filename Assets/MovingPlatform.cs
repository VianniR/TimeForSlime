using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private PlayerController playerController;
    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player") && Input.GetAxis("Horizontal") == 0)
        {  
            player.transform.parent = this.transform;
        }
        else
        {
            player.transform.parent = null;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            playerController.UnParent();
        }
    }


}
