using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnakeTarget : MonoBehaviour
{
    private Animator anim;
    public UnityEvent onHit;
    public UnityEvent onRelease;
    public float holdTime;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerWeapon") && collision.gameObject.name.Contains("Poison"))
        {
            onHit.Invoke();
            StartCoroutine(TargetTimer());
        }
    }

    IEnumerator TargetTimer()
    {
        anim.Play("ButtonPress");
        yield return new WaitForSeconds(0.3f);
        anim.speed = 1 / holdTime;
        yield return new WaitForSeconds(holdTime);
        anim.speed = 1;
        anim.Play("ButtonRelease");
        onRelease.Invoke();
    }
}
