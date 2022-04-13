using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabItem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public bool grabbedObject;
    bool hasNotBeenTouched = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player") && hasNotBeenTouched)
        {
            holdItem();
            transform.localPosition = new Vector2(.37f, -.11f);
            hasNotBeenTouched = false;
        }
    }

    void holdItem()
    {
        transform.parent = player.transform;
    }
}
