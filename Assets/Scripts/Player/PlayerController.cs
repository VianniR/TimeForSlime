
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Slime")]
    public float slimeCap; // Slime capacity
    public float slime;
    public RectTransform emptySlimeBar;
    public RectTransform slimeBar;
    public CardAnim tempCard;

    private Animator initialFallAnim;
    private Rigidbody2D playerRb;

    public bool finishedOpenAnimation = false;

    [Header("Health")]
    public float maxHealth; // Slime capacity
    public float health;
    public RectTransform emptyHealthBar;
    public RectTransform healthBar;
    public GameObject deathAnim;
    public Animator deathScreen;

    [Header("Player Info (Non-Mutable)")]
    public int enterGroundCollision;
    public Vector2 hitDirection;
    public bool stunned;
    public int moveDirection;
    public bool isDead;
    public bool isHoldingMouse;

    private Camera cam;
    private Vector2 mousePos;
    private bool onSlime = false;

    [Header("Level")]
    public LevelManager currLevel;
    public GameObject globalLight;
    public Light2D playerLight;

    int groundDetect = 0;

    public MasterController currMorphController;


    //public GameObject particleEmitterObject;


    private Vector2 gravity = new Vector2(0, -1);

    void Start()
    {
        initialFallAnim = GetComponent<Animator>();
        moveDirection = 1;
        //jumpParticle = particleEmitterObject.GetComponent<ParticleSystem>();
        playerRb = GetComponent<Rigidbody2D>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        slime = slimeCap;
        UpdateSlime(0);
        health = maxHealth;
        UpdateHealth(0);
        Physics2D.gravity = gravity * 9.8f;
    }

    void Update()
    {
        if(!initialFallAnim.GetCurrentAnimatorStateInfo(0).IsName("InitialFall"))
        {
            finishedOpenAnimation = true;
        }
    }

    public void UpdateSlime(float amount)
    {
        slime += amount;
        slime = Mathf.Clamp(slime, 0f, slimeCap);
        float scale = slime / slimeCap; // offset scale factor
        slimeBar.sizeDelta = new Vector2(emptySlimeBar.rect.width * scale, slimeBar.rect.height);
        slimeBar.localPosition = new Vector3((scale-1) * 0.5f * emptySlimeBar.rect.width, 0, 0);
    }

    public void UpdateHealth(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0f, maxHealth);
        float scale = health / maxHealth; // offset scale factor
        healthBar.sizeDelta = new Vector2(emptyHealthBar.rect.width * scale, healthBar.rect.height);
        healthBar.localPosition = new Vector3((scale - 1) * 0.5f * emptyHealthBar.rect.width, 0, 0);
        if(health == 0)
        {
            StartCoroutine(Death());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Slime") && !isDead)
        {
            UpdateSlime(other.gameObject.GetComponent<Slime>().slimeAmount);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("SpawnPoint"))
        {
            NewSpawn spawn = other.gameObject.GetComponent<NewSpawn>();
        }
    }


    public IEnumerator LightShrink(float time)
    {
        globalLight.SetActive(false);
        playerLight.gameObject.SetActive(true);
        float t = 0;
        while (t <= time)
        {
            float size = (1 - t / time);
            playerLight.pointLightOuterRadius = size;
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
        }
    }
    public IEnumerator LightGrow(float time)
    {
        for (float t = 0; t < time + 0.02f; t += 0.02f)
        {
            float size = (t / time) * 100;
            playerLight.pointLightOuterRadius = size;
            yield return new WaitForSeconds(0.02f);
        }
        playerLight.gameObject.SetActive(false);
        globalLight.gameObject.SetActive(true);
    }

    public void Die()
    {
        StartCoroutine(Death());
    }
    public IEnumerator Death()
    {
        isHoldingMouse = false;
        playerRb.velocity = Vector2.zero;
        isDead = true;
        playerRb.bodyType = RigidbodyType2D.Static;
        currMorphController.gameObject.SetActive(false);
        deathAnim.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        deathScreen.Play("FadeOut");
        yield return new WaitForSeconds(1);

         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        /*UpdateSlime(100);
        currLevel.ResetLevel();
        onSlime = false;

        StartCoroutine(LightGrow(0.4f));
        yield return new WaitForSeconds(0.3f);

        isDead = false;
        gravity = new Vector2(0, -1);
        Physics2D.gravity = gravity * 9.8f;*/
    }

    public IEnumerator CameraTransfer(float time, Tilemap tilemap, TileBase doorTile, Vector3 topPos, Vector3 botPos)
    {
        yield return new WaitForSeconds(time);
    }

    public void Hit(Vector2 force, int damage, float stunTimer)
    {
        UpdateHealth(-damage);
        if(damage > 0)
        {
            StartCoroutine(SlowTime());
        }
        StartCoroutine(Knockback(force, stunTimer));
        if(damage != 0)
            StartCoroutine(currMorphController.Invincible());
    }

    public IEnumerator Knockback(Vector2 force, float stunTimer)
    {
        stunned = true;
        playerRb.velocity = force;
        yield return new WaitForSeconds(stunTimer);
        stunned = false;
    }

    public IEnumerator SlowTime()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.01f);
        Time.timeScale = 1;
    }

    public void SetDirection(int newDir)
    {
        moveDirection = newDir;
        transform.localScale = new Vector3(moveDirection, transform.localScale.y, transform.localScale.z);
    }

    public void UnParent()
    {
        Debug.Log("lol");
        transform.parent = null;
    }
}
