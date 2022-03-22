using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private string groundState;
    private int currAnimationOverride;

    public void AssignValues(Animator animator, string idle)
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

    public void SetPriorityLevel(int level)
    {
        currAnimationOverride = level;
    }
}
