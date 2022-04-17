using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform top;
    public SpriteRenderer pole;
    public float speed;
    public Transform platformBase;

    public bool powered;
    private float progress;
    // Start is called before the first frame update
    void Start()
    {
        pole.drawMode = SpriteDrawMode.Tiled;
    }

    // Update is called once per frame
    void Update()
    {
        if(powered && progress < 1)
        {
            progress += (Time.deltaTime * speed) / (top.localPosition.y - 1);
        }
        else if(!powered && progress > 0)
        {
            progress -= (Time.deltaTime * speed) / (top.localPosition.y - 1);
        }

        pole.size = new Vector2(pole.size.x, Mathf.Lerp(1, top.localPosition.y, progress));
        pole.transform.localPosition = new Vector3(0, (pole.size.y - 1) * 0.5f, 0);
        platformBase.localPosition = new Vector3(0, (pole.size.y - 0.4f), 0);
    }

    public void Extend()
    {
        powered = true;
    }
    public void Retract()
    {
        powered = false;
    }
}
