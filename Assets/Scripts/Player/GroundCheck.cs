using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;


    public LayerMask groundLayer;
    private GridLayout grid;
    private Tilemap tilemap;

    public Collider2D referenceCollider;

    [SerializeField]
    float width;
    [SerializeField]
    float height;

    void Start()
    {
        grid = GameObject.Find("Grid").GetComponent<GridLayout>();
        tilemap = grid.gameObject.transform.Find("Ground").gameObject.GetComponent<Tilemap>();
        width = referenceCollider.bounds.size.x;
        height = referenceCollider.bounds.size.y;
    }


    public string LeftWallCollision()
    {
        float centerX = referenceCollider.bounds.center.x;
        float centerY = referenceCollider.bounds.center.y;
        Vector2 topPos = new Vector2(centerX - width / 2 - 0.07f, centerY + height / 2);
        Vector2 midPos = new Vector2(centerX - width / 2 - 0.07f, centerY);
        Vector2 botPos = new Vector2(centerX - width / 2 - 0.07f, centerY - height / 2);

        Collider2D bot = Physics2D.Linecast(player.transform.position, botPos, groundLayer).collider;
        Collider2D mid = Physics2D.Linecast(player.transform.position, midPos, groundLayer).collider;
        Collider2D top = Physics2D.Linecast(player.transform.position, topPos, groundLayer).collider;

        if(mid != null)
        {
            Vector3Int cellPos = grid.WorldToCell(midPos);
            return tilemap.GetSprite(cellPos).name;
        }
        else if (bot != null)
        {
            Vector3Int cellPos = grid.WorldToCell(botPos);
            return tilemap.GetSprite(cellPos).name;
        }
        else if (top != null)
        {
            Vector3Int cellPos = grid.WorldToCell(topPos);
            return tilemap.GetSprite(cellPos).name;
        }

        return "";
    }

    public string RightWallCollision()
    {
        float centerX = referenceCollider.bounds.center.x;
        float centerY = referenceCollider.bounds.center.y;
        Vector2 topPos = new Vector2(centerX + width / 2 + 0.07f, centerY + height / 2);
        Vector2 midPos = new Vector2(centerX + width / 2 + 0.07f, centerY);
        Vector2 botPos = new Vector2(centerX + width / 2 + 0.07f, centerY - height / 2);

        Collider2D bot = Physics2D.Linecast(player.transform.position, botPos, groundLayer).collider;
        Collider2D mid = Physics2D.Linecast(player.transform.position, midPos, groundLayer).collider;
        Collider2D top = Physics2D.Linecast(player.transform.position, topPos, groundLayer).collider;

        if (mid != null)
        {
            Vector3Int cellPos = grid.WorldToCell(midPos);
            return tilemap.GetSprite(cellPos).name;
        }
        else if (bot != null)
        {
            Vector3Int cellPos = grid.WorldToCell(botPos);
            return tilemap.GetSprite(cellPos).name;
        }
        else if (top != null)
        {
            Vector3Int cellPos = grid.WorldToCell(topPos);
            return tilemap.GetSprite(cellPos).name;
        }

        return "";
    }

    public string GroundCollision()
    {
        float centerX = referenceCollider.bounds.center.x;
        float centerY = referenceCollider.bounds.center.y;
        Vector2 rightPos = new Vector2(centerX + width / 2, centerY - height / 2 - 0.07f);
        Vector2 midPos = new Vector2(centerX, centerY - height / 2 - 0.07f);
        Vector2 leftPos = new Vector2(centerX - width / 2, centerY - height / 2 - 0.07f);

        Collider2D right = Physics2D.Linecast(player.transform.position, rightPos, groundLayer).collider;
        Collider2D mid = Physics2D.Linecast(player.transform.position, midPos, groundLayer).collider;
        Collider2D left = Physics2D.Linecast(player.transform.position, leftPos, groundLayer).collider;

        if (mid != null)
        {
            Vector3Int cellPos = grid.WorldToCell(midPos);
            return tilemap.GetSprite(cellPos).name;
        }
        else if (left != null)
        {
            Vector3Int cellPos = grid.WorldToCell(leftPos);
            return tilemap.GetSprite(cellPos).name;
        }
        else if (right != null)
        {
            Vector3Int cellPos = grid.WorldToCell(rightPos);
            return tilemap.GetSprite(cellPos).name;
        }

        return "";
    }

    public string CeilingCollision()
    {
        float centerX = referenceCollider.bounds.center.x;
        float centerY = referenceCollider.bounds.center.y;
        Vector2 rightPos = new Vector2(centerX + width / 2, centerY + height / 2 + 0.07f);
        Vector2 midPos = new Vector2(centerX, centerY + height / 2 + 0.07f);
        Vector2 leftPos = new Vector2(centerX - width / 2, centerY + height / 2 + 0.07f);

        Collider2D right = Physics2D.Linecast(player.transform.position, rightPos, groundLayer).collider;
        Collider2D mid = Physics2D.Linecast(player.transform.position, midPos, groundLayer).collider;
        Collider2D left = Physics2D.Linecast(player.transform.position, leftPos, groundLayer).collider;

        if (mid != null)
        {
            Vector3Int cellPos = grid.WorldToCell(midPos);
            return tilemap.GetSprite(cellPos).name;
        }
        else if (left != null)
        {
            Vector3Int cellPos = grid.WorldToCell(leftPos);
            return tilemap.GetSprite(cellPos).name;
        }
        else if (right != null)
        {
            Vector3Int cellPos = grid.WorldToCell(rightPos);
            return tilemap.GetSprite(cellPos).name;
        }

        return "";
    }
}
