using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardAnim : MonoBehaviour
{
    public Canvas canvas;
    public Camera cam;

    public Transform P2;
    public Transform playerCard;
    private Sprite playerCardSprite;
    public Sprite goobyCardSprite;

    public Transform defaultPos;
    public Animator playerCardAnim;

    public void PlayCardAnim(Sprite cardSprite, Vector3 position, Sprite playerCard)
    {
        playerCardSprite = playerCard;
        gameObject.GetComponent<Image>().sprite = cardSprite;
        RectTransform CanvasRect = canvas.GetComponent<RectTransform>();

        Vector2 ViewportPosition = cam.WorldToViewportPoint(position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

        transform.localPosition = WorldObject_ScreenPosition;

        StartCoroutine(CardTransform());
    }

    IEnumerator CardTransform()
    {
        Vector2 P1 = transform.localPosition;
        for(float t = 0; t <= 1; t+=0.05f)
        {
            Vector2 A = Vector2.Lerp(P1, P2.localPosition, t);
            Vector2 B = Vector2.Lerp(P2.localPosition, playerCard.localPosition, t);

            transform.localPosition = Vector2.Lerp(A, B, t);
            yield return new WaitForSeconds(0.02f);
        }
        transform.localPosition = defaultPos.position;

        playerCardAnim.Play("Flip");
        yield return new WaitForSeconds(0.208f);
        playerCard.GetChild(1).SetAsFirstSibling();
        playerCard.GetChild(0).GetComponent<Image>().sprite = playerCardSprite;
        yield return new WaitForSeconds(0.416f);
        playerCard.GetChild(1).SetAsFirstSibling();
    }

    public IEnumerator CardUnmorph()
    {
        playerCardAnim.Play("Flip");
        yield return new WaitForSeconds(0.208f);
        playerCard.GetChild(1).SetAsFirstSibling();
        playerCard.GetChild(0).GetComponent<Image>().sprite = goobyCardSprite;
        yield return new WaitForSeconds(0.416f);
        playerCard.GetChild(1).SetAsFirstSibling();
    }
}
