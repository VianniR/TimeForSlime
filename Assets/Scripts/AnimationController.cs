using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    public string groundState;
    private int currAnimationOverride;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public bool PlayAnim(string animation, int priority)
    {
        if(priority > currAnimationOverride)
        {
            animator.Play(animation);
            currAnimationOverride = priority;
            if(animation == groundState)
            {
                currAnimationOverride = 0;
            }
            return true;
        }

        return false;
    }

    public void SetPriorityLevel(int level)
    {
        currAnimationOverride = level;
    }
}
