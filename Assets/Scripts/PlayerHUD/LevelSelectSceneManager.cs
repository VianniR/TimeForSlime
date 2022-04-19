using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] levelAreas;
    public Transform[] allLevels;
    public Transform player;
    public PlayerController playerScript;
    void Start()
    {
        if(PlayerPrefs.HasKey("CurrLevel"))
        {
            LoadOneLevel(1);
            player.position = allLevels[PlayerPrefs.GetInt("CurrLevel") - 1].position;
            //playerScript.spawnPoint = allLevels[PlayerPrefs.GetInt("Currlevel")].Find("Spawn");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadOneLevel(int index)
    {
        playerScript.currLevel = levelAreas[index].GetComponent<LevelManager>();
        for (int i = 0; i < levelAreas.Length; i++)
        {
            if(i != index)
            {
                levelAreas[i].SetActive(false);
            }  
        }
        levelAreas[index].SetActive(true);
    }
}