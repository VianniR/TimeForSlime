using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gooby;
    public GameObject player;
    public GameObject initialFallCam;
    public GameObject cam1;

    private Animator goobyAnimator;
    private Animation cameraAnimation;

    void Start()
    {
        goobyAnimator = gooby.GetComponent<Animator>();
        cameraAnimation = initialFallCam.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void enableAnimators()
    {
        player.GetComponent<Animator>().enabled = false;
        goobyAnimator.enabled = true;
        cameraAnimation.enabled = true;
        
        resetGoobyPostion();
    }

    void resetGoobyPostion()
    {
        player.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    void initialFallCamToCam1()
    {
        this.gameObject.SetActive(false);
        cam1.SetActive(true);
    }
    
}
