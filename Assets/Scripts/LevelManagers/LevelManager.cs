using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelReset[] resets;
    public GameObject slimePrefab;

    public virtual void ResetLevel()
    {
        foreach(LevelReset r in resets)
        {
            r.OnLevelReset();
        }
    }

    void Start()
    {
        ResetLevel();
    }
}
