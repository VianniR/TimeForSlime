using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenLocks : MonoBehaviour
{
    public GameObject golden1;
    public GameObject golden2;
    public GameObject swampUnlocked;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("LevelProgress"))
        {
            if(PlayerPrefs.GetInt("LevelProgress") > 0)
            {
                golden1.SetActive(true);
                swampUnlocked.SetActive(true);
            }
            if (PlayerPrefs.GetInt("LevelProgress") > 1)
            {
                golden2.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
