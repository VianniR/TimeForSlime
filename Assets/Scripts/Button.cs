using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : LevelReset
{
    public Transform RedThing;
    public UnityEvent OnClick;

    public UnityEvent OnReset;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            RedThing.localPosition = new Vector3(0, -0.2f, 0);
            OnClick.Invoke();
        }
    }

    public override void OnLevelReset()
    {
        RedThing.localPosition = new Vector3(0, 0.1f, 0);
        OnReset.Invoke();
    }
}
