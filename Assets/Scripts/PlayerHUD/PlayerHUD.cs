using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHUD : MonoBehaviour
{
    public GameObject EscapeMenu;
    public GameObject PlayerMenu;
    public int currLevelNum;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (EscapeMenu.activeSelf)
            {
                Time.timeScale = 1;
                EscapeMenu.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                EscapeMenu.SetActive(true);
            }
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (EscapeMenu.activeSelf) { }
            else if(PlayerMenu.activeSelf)
            {
                Time.timeScale = 1;
                PlayerMenu.SetActive(false);
            }
            else if(!PlayerMenu.activeSelf)
            {
                Time.timeScale = 0;
                PlayerMenu.SetActive(true);
            }
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        EscapeMenu.SetActive(false);
    }

    public void ReturnToLevelSelect()
    {
        Time.timeScale = 1;
        //Temp
        SceneManager.LoadScene("Level Select");
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        //Temp
        SceneManager.LoadScene("Title Screen");
    }
}
