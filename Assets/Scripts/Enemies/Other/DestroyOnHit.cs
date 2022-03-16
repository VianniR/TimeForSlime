using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour
{
    [SerializeField]
    private string tag1;
    private string tag2;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(tag1))
        {
            Destroy(gameObject);
        }
        if(tag2 != null && other.CompareTag(tag2))
        {
            Destroy(gameObject);
        }
    }
}
