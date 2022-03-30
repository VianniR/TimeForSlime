using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    public KeyCode interactKey;
    public List<UnityEvent> events;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(interactKey))
        {
            foreach(UnityEvent e in events)
            {
                e.Invoke();
            }
        }
    }
}
