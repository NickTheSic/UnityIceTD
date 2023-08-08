using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBase : MonoBehaviour
{

    public SpriteRenderer Spr;

    public int xIndex;
    public int yIndex;

    public Tile thisTile;
    public Tile.Type tileType;

    public bool canPlaceTower;
    public bool hasTower;

    GameObject playerController;
    PlayerController pc;

    public GameObject tower;
    GameObject thePlacedTower;

    // Start is called before the first frame update
    void Start()
    {
        thePlacedTower = null;
        Spr = this.gameObject.GetComponent<SpriteRenderer>();
        playerController = GameObject.FindGameObjectWithTag("PC");
        pc = playerController.GetComponent<PlayerController>();

        Reset();
    }

    public void Reset()
    {
        if (thePlacedTower != null)
            Destroy(thePlacedTower);

        thePlacedTower = null;
        hasTower = false;

        if (tileType == Tile.Type.Placeable)
            canPlaceTower = true;

        SetType(thisTile, xIndex, yIndex);
    }
    // Update is called once per frame
    void Update()
    {
        if (tileType == Tile.Type.Placeable && canPlaceTower)
        {

        }
    }

    private void OnMouseOver()
    {
       if (tileType == Tile.Type.Placeable)
        {
            if (canPlaceTower && pc.TowerActiveToClick())
                Spr.color = Color.green;
        }
    }

    private void OnMouseDown()
    {
        if (tileType == Tile.Type.Placeable && canPlaceTower && pc.TowerActiveToClick())
        {
            GameObject newTower = Instantiate(tower, new Vector2(transform.position.x + 0.5f, transform.position.y + 0.5f), Quaternion.identity);
            BaseTower bt = newTower.GetComponent<BaseTower>();
            thePlacedTower = newTower;
            bt.SetTower(pc.GetTower());
            PlayerController.Money -= pc.GetTower().BaseCost;
            pc.DeselectTower();
            canPlaceTower = false;
            hasTower = true;
        }
    }

    private void OnMouseExit()
    {
        if (tileType == Tile.Type.Placeable)
        {
            Spr.color = Color.white;
        }
    }

    public void SetType(Tile tile, int x, int y)
    {
        Spr.sprite = tile.tile;
        thisTile = tile;
        tileType = tile.State;
        if (y != 0 || x < 15)
            canPlaceTower = (tileType == Tile.Type.Placeable);
        if (y == 0 || x == 15 || x == 16 || (y >= 7 && x >=13))
            canPlaceTower = false;
        xIndex = x;
        yIndex = y;
    }
}
