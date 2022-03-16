using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCard : MonoBehaviour
{
    public GameObject morph;
    public Sprite morphCard;
    private PlayerController player;
    private CardAnim tempCard;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        tempCard = player.tempCard;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Morph();
        }
    }

    public void Morph()
    {
        tempCard.PlayCardAnim(transform.parent.gameObject.GetComponent<SpriteRenderer>().sprite, transform.position, morphCard);
        player.Morph(morph);
        Destroy(transform.parent.gameObject);
    }
}
