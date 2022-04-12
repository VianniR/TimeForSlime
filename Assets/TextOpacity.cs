using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextOpacity : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Image image;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        image.color = new Color32(255, 255, 255, (player.transform.position.x - transform.position.x));
    }
}
