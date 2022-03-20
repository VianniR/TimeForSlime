using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private KnockbackData knockData;
    void Start()
    {
        knockData = GetComponent<KnockbackData>();
        knockData.targetTransform = transform.parent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
