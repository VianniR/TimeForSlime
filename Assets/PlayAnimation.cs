using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject gooler;
    public GameObject bucket;
    public string animation;

    public GameObject anim2Cam;
    public GameObject anim1Cam;
    private Animation anim2CamAnimation;
    private Animator bucketAnimator;
    private Animator goolerAnimator;

    
    void Start()
    {
        bucketAnimator = bucket.GetComponent<Animator>();
        goolerAnimator = gooler.GetComponent<Animator>();
        anim2CamAnimation = anim2Cam.GetComponent<Animation>();
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            bucket.transform.parent = null;
            bucketAnimator.enabled = true;
            anim1Cam.SetActive(false);
            anim2Cam.SetActive(true);
            anim2CamAnimation.Play();
            goolerAnimator.Play("SplashedOn");
            
        }
    }
}
