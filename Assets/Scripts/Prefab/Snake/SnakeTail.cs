using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    private float snakeTime;
    private SnakeGame gameController;
    public Animator snakeAnim;
    public bool bend;
    // Start is called before the first frame update
    public void SetVars(SnakeGame gameInstance, float timer, bool bending)
    {
        snakeTime = timer;
        gameController = gameInstance;
        bend = bending;
    }

    // Update is called once per frame

    IEnumerator SnakeWiggle()
    {
        yield return new WaitForSeconds(0.08333f);
        yield return new WaitForSeconds(snakeTime);
        if(bend)
            snakeAnim.Play("BendEnd");
        else
            snakeAnim.Play("TailEnd");
        yield return new WaitForSeconds(0.08333f);
        Destroy(gameObject);
    }
}
