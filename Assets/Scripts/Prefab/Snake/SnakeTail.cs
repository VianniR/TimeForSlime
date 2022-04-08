using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    private float snakeTime;
    private SnakeGame gameController;
    public Animator snakeAnim;
    public bool bend;
    public Vector2 lookDirection;
    private Collider2D snakeCollider;
    public void SetVars(SnakeGame gameInstance, float timer, bool bending, Vector2 direction)
    {
        snakeTime = timer;
        gameController = gameInstance;
        bend = bending;
        lookDirection = direction;
        StartCoroutine(SnakeWiggle());
        snakeCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame

    IEnumerator SnakeWiggle()
    {
        yield return new WaitForSeconds(0.0833f);
        snakeCollider.enabled = true;
        yield return new WaitForSeconds(snakeTime - 0.0833f);
        if(bend)
            snakeAnim.Play("BendEnd");
        else
            snakeAnim.Play("TailEnd");
        yield return new WaitForSeconds(0.08333f);
        gameController.snakeTiles.Remove(gameObject);
        Destroy(gameObject);
    }
}
