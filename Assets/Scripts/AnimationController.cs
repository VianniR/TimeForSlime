using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController
{
    public Animator animator;
    public string groundState;
    private int currAnimationOverride;

    public AnimationController(Animator animator, string idle)
    {
        this.animator = animator;
        groundState = idle;
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

    public void UpdateAnim()
    {
        if(animator.IsInTransition(0))
        {
            currAnimationOverride = 0;
        }
    }
}
