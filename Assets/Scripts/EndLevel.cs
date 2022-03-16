using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public float eventNum;

    private bool isPlayingAnim;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        isPlayingAnim = false;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isPlayingAnim)
        {
            StartCoroutine(LevelClose());
        }
    }

    IEnumerator LevelClose()
    {
        isPlayingAnim = true;
        StartCoroutine(player.LightShrink(0.4f));
        yield return new WaitForSeconds(0.8f);
        if (PlayerPrefs.GetFloat("Events") < eventNum)
        {
            PlayerPrefs.SetFloat("Events", eventNum);
        }
        SceneManager.LoadScene("Level Select");
    }
}
