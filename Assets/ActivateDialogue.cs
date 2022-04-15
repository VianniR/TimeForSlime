using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDialogue : MonoBehaviour
{
    // Start is called before the first frame update
    public DialogueTrigger dialogueTrigger;
    public GameObject canvas;
    bool touchingCollider = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("touched");
        canvas.SetActive(true);
        touchingCollider = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {   
        touchingCollider = false;
        canvas.SetActive(false);
    }
    void Update(){
    if(Input.GetKey(KeyCode.R) && touchingCollider)
    {
        dialogueTrigger.TriggerDialogue();
    }

    }
}
