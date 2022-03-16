using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSlime : LevelReset
{
    public GameObject slimePrefab;
    public override void OnLevelReset()
    {
        try
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        catch { }
        Instantiate(slimePrefab, transform.position, slimePrefab.transform.rotation, transform);
    }
}
