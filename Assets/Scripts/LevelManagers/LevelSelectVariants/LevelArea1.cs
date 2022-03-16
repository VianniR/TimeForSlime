using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelArea1 : LevelManager
{
    public Transform slime1Pos;

    public override void ResetLevel()
    {
        base.ResetLevel();
        try { Destroy(slime1Pos.GetChild(0).gameObject); }
        catch { }
            
        if(PlayerPrefs.HasKey("Events") && PlayerPrefs.GetFloat("Events") > 1)
        {
            Instantiate(slimePrefab, slime1Pos.position, slimePrefab.transform.rotation, slime1Pos);
        }
    }
}
