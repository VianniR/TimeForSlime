using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrippySlime : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject slimeParticle;
    public GameObject self;
    void Start()
    {
        //limeParticle = GameObject.Find("drippySlime");
        startDrip();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startDrip()
    {
        StartCoroutine(Drip());
    }
    IEnumerator Drip()
    {
        while(true){
        Instantiate(slimeParticle, self.transform);
        yield return new WaitForSeconds(1f);
        }
        
        
    }
}
