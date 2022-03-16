using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event2 : MonoBehaviour
{
    public GameObject moveCam;
    public Animator door;

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
        if (eventNum > 2)
        {
            door.Play("DoorOpen");
        }
        else if (eventNum > 1.5f)
        {
            StartCoroutine(LongEvent());
        }
    }

    IEnumerator LongEvent()
    {
        yield return new WaitForSeconds(1f);
        moveCam.SetActive(true);
        yield return new WaitForSeconds(1f);
        door.Play("DoorOpen");
        yield return new WaitForSeconds(1f);
        moveCam.SetActive(false);
        PlayerPrefs.SetFloat("Events", 2.5f);
    }
}
