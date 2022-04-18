using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : LevelReset
{
    public UnityEvent OnClick;
    public UnityEvent OnRelease;

    public UnityEvent OnReset;
    public Collider2D refereceCollider;
    public LayerMask detectionLayers;
    private Transform boxcastStart;

    private bool pressed;
    private bool triggered;
    private Animator buttonAnim;

    private void Start()
    {
        buttonAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        bool pressed = Physics2D.BoxCast(refereceCollider.bounds.center, refereceCollider.bounds.size, 0, Vector2.up, .1f, detectionLayers);


        if (pressed && !triggered)
        {
            triggered = true;
            OnClick.Invoke();
            buttonAnim.Play("ButtonPress");
        }
        else if(!pressed && triggered)
        {
            triggered = false;
            OnRelease.Invoke();
            buttonAnim.Play("ButtonRelease");
        }
    }

    public override void OnLevelReset()
    {
        OnReset.Invoke();
    }
}
