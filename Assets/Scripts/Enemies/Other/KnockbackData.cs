using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackData : MonoBehaviour
{
    public float force;
    public int damage;
    public float stunTime;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
            Vector2 hitDirection = new Vector2(Mathf.Sign(player.transform.position.x - transform.position.x), Mathf.Sign(player.transform.position.y - transform.position.y));
            player.Hit(hitDirection * force, damage, stunTime);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
            Vector2 hitDirection = new Vector2(Mathf.Sign(player.transform.position.x - transform.position.x), Mathf.Sign(player.transform.position.y - transform.position.y));
            player.Hit(hitDirection * force, damage, stunTime);
        }
    }
}
