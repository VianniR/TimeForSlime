using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private bool isPlayingAnim = false;
    public Animator screenFade;
    public void ChangeSceneTo(string nextScene)
    {
        if (!isPlayingAnim)
        {
            StartCoroutine(LevelClose(nextScene));
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LevelClose(string nextScene)
    {
        isPlayingAnim = true;
        screenFade.Play("FadeToBlack");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextScene);
        isPlayingAnim = false;

        //Temp
        PlayerPrefs.DeleteKey("CurrLevel");
        PlayerPrefs.SetFloat("Events", 0);
    }

    public void QuitGameLOle()
    {
        StartCoroutine(QuitGame());
    }

    public IEnumerator QuitGame()
    {
        isPlayingAnim = true;
        screenFade.Play("FadeToBlack");
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
}