using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorphManager : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerController player;

    [Header("Morph")]
    public GameObject gooby;
    public GameObject morphBubble;
    public string lastKilled;
    public GameObject currDNA;
    public CardAnim tempCard;
    public Animator DNAHolder;

    private GameObject currMorph;
    private Rigidbody2D playerRb;

    void Start()
    {
        player = GetComponent<PlayerController>();
        player.currMorphController = gooby.GetComponent<MasterController>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && currMorph != null && !morphBubble.activeSelf)
        {
            DNAHolder.Play("Q");
            StartCoroutine(Unmorph());
        }
        if (currDNA != null && Input.GetKeyDown(KeyCode.E) && player.slime >= 25)
        {
            DNAHolder.Play("E");
            StartCoroutine(Morph(currDNA));
        }
    }

    public IEnumerator Morph(GameObject newMorph)
    {
        player.UpdateSlime(-25);
        float bubbleSize = newMorph.GetComponent<MasterController>().morphSize;
        morphBubble.transform.localScale = new Vector3(bubbleSize, bubbleSize, 1);
        morphBubble.SetActive(true);
        StartCoroutine(tempCard.CardMorph());

        yield return new WaitForSeconds(0.25f);
        if (currMorph != null)
        {
            Destroy(currMorph);
        }
        gooby.SetActive(false);
        currMorph = Instantiate(newMorph, transform);
        player.currMorphController = currMorph.GetComponent<MasterController>();
        yield return new WaitForSeconds(0.4f);
        morphBubble.SetActive(false);
    }

    public IEnumerator Unmorph()
    {
        playerRb.mass = 1;
        float bubbleSize = player.currMorphController.morphSize;
        morphBubble.transform.localScale = new Vector3(bubbleSize, bubbleSize, 1);
        morphBubble.SetActive(true);
        StartCoroutine(tempCard.CardUnmorph());

        yield return new WaitForSeconds(0.25f);
        Destroy(currMorph);
        currMorph = null;
        player.currMorphController = gooby.GetComponent<MasterController>();
        gooby.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        morphBubble.SetActive(false);
    }

    public GameObject getCurrMorph()
    {
        return currMorph;
    }
}
