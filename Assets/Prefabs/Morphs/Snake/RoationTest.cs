using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoationTest : MonoBehaviour
{
    public Vector4 rot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = new Quaternion(rot.x, rot.y, rot.z, rot.w);
    }
}
