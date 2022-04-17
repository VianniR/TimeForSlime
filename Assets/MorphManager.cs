using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorphManager : MonoBehaviour
{
    // Start is called before the first frame update
    public string lastKilled;
    public GameObject ratMorph;

    public GameObject snakeMorph;
    public Sprite morphCard;
    private PlayerController player;
    private CardAnim tempCard;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && player.slime >= 25)
        {
            Morph();
        }
    }

    public void Morph()
    {
        //tempCard.PlayCardAnim(transform.parent.gameObject.GetComponent<SpriteRenderer>().sprite, transform.position, morphCard);
        if(lastKilled.Equals("Rat"))
        {
            player.Morph(ratMorph);
        }

        else if(lastKilled.Equals("Snake"))
        {
            player.Morph(snakeMorph);
        }
        //Destroy(transform.parent.gameObject);
    }

  
}
