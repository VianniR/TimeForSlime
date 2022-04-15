using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    public int slimeCounter = 0;
    public Sprite filledSprite;
    private SpriteRenderer selfRenderer;
    // Start is called before the first frame update
    void Start()
    {
        selfRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("SlimeParticle"))
        {
            if(slimeCounter != 3)
            {
                slimeCounter++;
            }
            if(slimeCounter == 3)
            {
                selfRenderer.sprite = filledSprite;
            }
        }
    } 
}
