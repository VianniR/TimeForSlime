using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public UnityEvent resets;
    public GameObject slimePrefab;

    public virtual void ResetLevel()
    {
        resets.Invoke();
    }

    void Start()
    {
        ResetLevel();
    }
}
