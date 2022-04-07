using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneScript : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerController playerControllerScript;
    public bool initialAnimationDone;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        initialAnimationDone = playerControllerScript.finishedOpenAnimation;
        if (initialAnimationDone)
        {
            GetComponent<Animator>().enabled = true;
        }
        else
        {
            GetComponent<Animator>().enabled = false;
       }
    }
}
