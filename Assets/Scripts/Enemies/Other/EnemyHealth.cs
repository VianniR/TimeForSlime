using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public string name;

    private MorphManager morphManager;
    public float maxHealth;
    public float health;
    private bool isDead;
    public GameObject enemyCard;

    private PlayerController player;
    private MasterEnemy enemyScript;


    private void Start()
    {
        isDead = false;
        health = maxHealth;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        morphManager = GameObject.Find("Morph Manager").GetComponent<MorphManager>();
        enemyScript = gameObject.GetComponent<MasterEnemy>();
        
    }

    private void Update()
    {
        if(health <= 0 && !isDead)
        {
            
            isDead = true;
            StartCoroutine(Death());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerWeapon"))
        {
            Weapon weapon = collision.GetComponent<Weapon>();
            PlayerController playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
            health -= weapon.damage;
            Knockback(playerScript.hitDirection, weapon.force);
            Debug.Log("hit");
        }
    }

    IEnumerator Death()
    {
        
        yield return new WaitForSeconds(0.1f);
        if(morphManager.lastKilled.Equals(name) == false){
            //morphManager.lastKilled = name;
            Instantiate(enemyCard, transform.position, enemyCard.transform.rotation);
        }
        Destroy(gameObject);
    }

    public void Knockback(Vector2 direction, float force)
    {
        StartCoroutine(enemyScript.StunEnemy());
        Vector2 lole = new Vector2(direction.x * force, direction.y * force / 1.5f);
        gameObject.GetComponent<Rigidbody2D>().velocity = lole;
    }

    public float CurrHealth()
    {
        return health;
    }
}
