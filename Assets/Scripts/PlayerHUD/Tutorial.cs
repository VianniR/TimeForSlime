using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private int progress;
    public GameObject Text0;
    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;
    public GameObject Text4;
    // Start is called before the first frame update
    void Awake()
    {
        progress = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(progress == 0 && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)))
        {
            Text0.SetActive(false);
            Text1.SetActive(true);
            progress = 1;
        }
        else if (progress == 1 && (Input.GetKeyDown(KeyCode.Space)))
        {
            Text1.SetActive(false);
            Text2.SetActive(true);
            progress = 2;
        }
        else if (progress == 2 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Text2.SetActive(false);
            progress = 3;
        }
        else if (progress == 3 && Input.GetKeyDown(KeyCode.F))
        {
            Text3.SetActive(true);
            progress = 4;
        }
        else if (progress == 4 && Input.GetKeyDown(KeyCode.E))
        {
            Text3.SetActive(false);
            Text4.SetActive(true);
            progress = 5;
        }
        else if (progress == 5 && Input.GetKeyDown(KeyCode.Q))
        {
            Text4.SetActive(false);
            progress = 6;
        }
    }
}
