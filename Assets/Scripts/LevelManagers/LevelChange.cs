using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Tilemaps;

public class LevelChange : MonoBehaviour
{
    public GameObject currLevel;
    public GameObject nextLevel;
    public float transferTime;
    public Tile doorTile;
    public Transform doorTop;
    public Transform doorBottom;

    private GridLayout grid;
    private Tilemap tilemap;

    private void Start()
    {
        grid = GameObject.Find("Grid").GetComponent<GridLayout>();
        tilemap = grid.transform.GetChild(0).gameObject.GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CinemachineBrain camera = GameObject.Find("Main Camera").GetComponent<CinemachineBrain>();
            camera.m_DefaultBlend.m_Time = transferTime;
            PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
            player.currLevel = nextLevel.GetComponent<LevelManager>();
            player.UpdateSlime(player.slimeCap);
            //StartCoroutine(player.CameraTransfer(transferTime, tilemap, doorTile, doorTop.position, doorBottom.position));
            StartCoroutine(DoorFill(transferTime));
            nextLevel.SetActive(true);
        }
    }
    public IEnumerator DoorFill(float time)
    {
        yield return new WaitForSeconds(time);
        if (doorTile != null)
            BoxFill(tilemap, doorTile, doorTop.position, doorBottom.position);
        currLevel.SetActive(false);
    }

    public void BoxFill(Tilemap map, TileBase tile, Vector3Int start, Vector3Int end)
    {
        //Determine directions on X and Y axis
        var xDir = start.x < end.x ? 1 : -1;
        var yDir = start.y < end.y ? 1 : -1;
        //How many tiles on each axis?
        int xCols = 1 + Mathf.Abs(start.x - end.x);
        int yCols = 1 + Mathf.Abs(start.y - end.y);
        //Start painting
        for (var x = 0; x < xCols; x++)
        {
            for (var y = 0; y < yCols; y++)
            {
                var tilePos = start + new Vector3Int(x * xDir, y * yDir, 0);
                map.SetTile(tilePos, tile);
            }
        }
    }

    //Small override, to allow for world position to be passed directly
    public void BoxFill(Tilemap map, TileBase tile, Vector3 start, Vector3 end)
    {
        BoxFill(map, tile, map.WorldToCell(start), map.WorldToCell(end));
    }
}
