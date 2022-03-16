using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event1 : MonoBehaviour
{
    public GameObject moveCam;
    public Transform slimePos;
    public GameObject slimePrefab;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Events"))
        {
            PlayEvent(PlayerPrefs.GetFloat("Events"));
        }
    }

    // Update is called once per frame
    public void PlayEvent(float eventNum)
    {
        if(eventNum > 1)
        {

        }
        else if(eventNum > 0)
        {
            StartCoroutine(LongEvent());
        }
    }

    IEnumerator LongEvent()
    {
        yield return new WaitForSeconds(1f);
        moveCam.SetActive(true);
        yield return new WaitForSeconds(1f);
        Instantiate(slimePrefab, slimePos.position, slimePrefab.transform.rotation, slimePos);
        moveCam.SetActive(false);
        PlayerPrefs.SetFloat("Events", 1.5f);
    }
}
