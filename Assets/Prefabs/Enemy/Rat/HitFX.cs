using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitFX : MonoBehaviour
{
    public string targetTag;
    public GameObject hitFX;
    public Transform thisTransform;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(targetTag))
        {
                Transform theScratch = Instantiate(hitFX, collision.transform.position, hitFX.transform.rotation).transform;
                theScratch.localScale = new Vector3(Mathf.Sign(thisTransform.localScale.x) * theScratch.localScale.x, theScratch.localScale.y, 1);
        }
    }
}
