using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverOnMorph : MonoBehaviour
{
    public GameObject enabledObject;
    public string morphScript;
    private MorphManager player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<MorphManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && player.getCurrMorph() != null && player.getCurrMorph().name.Contains(morphScript))
        {
            enabledObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enabledObject.SetActive(false);
        }
    }
}
