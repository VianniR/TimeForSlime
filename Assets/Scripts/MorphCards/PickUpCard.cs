using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCard : MonoBehaviour
{
    public string name;
    private MorphManager morphManager;
    public GameObject morph;
    public Sprite morphCard;
    private PlayerController player;
    private CardAnim tempCard;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        morphManager = GameObject.Find("Player").GetComponent<MorphManager>();
        tempCard = player.tempCard;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            AssignDNA();
        }
    }

    public void AssignDNA()
    {
        morphManager.lastKilled = name;
        morphManager.currDNA = morph;
        tempCard.PlayCardAnim(transform.parent.gameObject.GetComponent<SpriteRenderer>().sprite, transform.position, morphCard);
        //player.Morph(morph);
        Destroy(transform.parent.gameObject);
    }
}
