using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SelectLevel : MonoBehaviour
{
    public string levelName;
    public int levelNum;

    private bool isPlayingAnim;
    private PlayerController player;
    public GameObject Text;
    // Start is called before the first frame update
    void Start()
    {
        isPlayingAnim = false;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isPlayingAnim)
        {
            StartCoroutine(LevelClose());
        }
    }

    IEnumerator LevelClose()
    {
        Text.SetActive(false);
        isPlayingAnim = true;
        StartCoroutine(player.LightShrink(0.4f));
        yield return new WaitForSeconds(0.8f);
        PlayerPrefs.SetInt("CurrLevel", levelNum);
        SceneManager.LoadScene(levelName);
    }
}
