using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionOnAwake : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject other;
    void Start()
    {
        transform.position = other.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
