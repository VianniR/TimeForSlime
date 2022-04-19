using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public int eventNum;

    private bool isPlayingAnim;
    public Animator lightAnim;
    // Start is called before the first frame update
    void Start()
    {
        isPlayingAnim = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isPlayingAnim)
        {
            StartCoroutine(LevelClose());
        }
    }

    IEnumerator LevelClose()
    {
        isPlayingAnim = true;
        lightAnim.Play("FadeOut");
        yield return new WaitForSeconds(0.8f);
        if (PlayerPrefs.HasKey("LevelProgress"))
        {
            if (PlayerPrefs.GetInt("LevelProgress") < eventNum)
                PlayerPrefs.SetInt("LevelProgress", eventNum);
        }
        else
        {
            PlayerPrefs.SetInt("LevelProgress", eventNum);
        }
        SceneManager.LoadScene("Title Screen");
    }
}
