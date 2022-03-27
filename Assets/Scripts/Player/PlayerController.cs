
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public float slimeCap = 100; // Slime capacity

    public float slime;

    public RectTransform emptySlimeBar;
    private Transform slimeBar;
    private Rigidbody2D playerRb;
    public CardAnim tempCard;



    public int enterGroundCollision;

    public bool isHoldingMouse;
    private Camera cam;
    private Vector2 mousePos;

    public bool isDead;
    private bool onSlime = false;

    public Transform spawnPoint;
    public LevelManager currLevel;
    public GameObject globalLight;
    public Transform playerLight;

    int groundDetect = 0;

    public GameObject gooby;
    private GameObject currMorph;
    private MasterController currMorphController;
    private bool onMorphCard;

    public Vector2 hitDirection;
    public bool stunned;

    //public GameObject particleEmitterObject;


    private Vector2 gravity = new Vector2(0, -1);

    void Start()
    {
        //jumpParticle = particleEmitterObject.GetComponent<ParticleSystem>();
        slimeBar = emptySlimeBar.GetChild(0);
        playerRb = GetComponent<Rigidbody2D>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        slime = 100;
        UpdateSlime(0);
        transform.position = spawnPoint.position;
        Physics2D.gravity = gravity * 9.8f;
        currMorphController = gooby.GetComponent<MasterController>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && currMorph != null && !onMorphCard)
        {
            Unmorph();
        }
    }

    public void UpdateSlime(float amount)
    {
        slime += amount;
        slime = Mathf.Clamp(slime, 0f, slimeCap);
        float scale = slime / slimeCap; // offset scale factor
        slimeBar.localScale = new Vector2(slimeBar.localScale.x, scale);
        slimeBar.localPosition = new Vector3(0, (scale-1) * 0.5f * emptySlimeBar.rect.height, 0);
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
            spawnPoint = spawn.spawnPoint;
        }
        else if (other.gameObject.CompareTag("MorphCard"))
        {
            onMorphCard = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MorphCard"))
        {
            onMorphCard = false;
        }
    }


    public IEnumerator LightShrink(float time)
    {
        globalLight.SetActive(false);
        playerLight.gameObject.SetActive(true);
        for (float t = 0; t < time + 0.02f; t += 0.02f)
        {
            float size = (1 - t / time) * 100;
            playerLight.localScale = new Vector3(size, size, 1);
            yield return new WaitForSeconds(0.02f);
        }
    }
    public IEnumerator LightGrow(float time)
    {
        for (float t = 0; t < time + 0.02f; t += 0.02f)
        {
            float size = (t / time) * 100;
            playerLight.localScale = new Vector3(size, size, 1);
            yield return new WaitForSeconds(0.02f);
        }
        playerLight.gameObject.SetActive(false);
        globalLight.gameObject.SetActive(true);
    }

    public IEnumerator Death()
    {
        isHoldingMouse = false;
        playerRb.velocity = Vector2.zero;
        Physics2D.gravity = Vector2.zero;
        isDead = true;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(LightShrink(0.4f));
        yield return new WaitForSeconds(0.8f);

        UpdateSlime(100);
        transform.position = spawnPoint.position;
        currLevel.ResetLevel();
        onSlime = false;

        StartCoroutine(LightGrow(0.4f));
        yield return new WaitForSeconds(0.3f);

        isDead = false;
        gravity = new Vector2(0, -1);
        Physics2D.gravity = gravity * 9.8f;
    }

    public IEnumerator CameraTransfer(float time, Tilemap tilemap, TileBase doorTile, Vector3 topPos, Vector3 botPos)
    {
        yield return new WaitForSeconds(time);
    }

    public void Morph(GameObject newMorph)
    {
        StartCoroutine(MorphRoutine(newMorph));
    }

    public IEnumerator MorphRoutine(GameObject newMorph)
    {
        yield return new WaitForSeconds(0.7f);
        if (currMorph != null)
        {
            Destroy(currMorph);
        }
        gooby.SetActive(false);
        currMorph = Instantiate(newMorph, transform);
        currMorphController = currMorph.GetComponent<MasterController>();
    }

    public void Unmorph()
    {
        StartCoroutine(tempCard.CardUnmorph());
        Destroy(currMorph);
        currMorph = null;
        currMorphController = gooby.GetComponent<MasterController>();
        gooby.SetActive(true);
    }

    public void Hit(Vector2 force, int damage, float stunTimer)
    {
        UpdateSlime(-damage);
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
}
