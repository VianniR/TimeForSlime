using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetroyAfterTime : MonoBehaviour
{
    public float maxTime;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
