using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Tile[] tileTypes;

    public GameObject tileObject;

    public const int X = 18;
    public const int Y = 10;
    public int[,] Grid = new int[Y, X]
        {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 8, 3, 5, 0, 0, 8, 1, 1, 5, 0, 0, 0, 0, 0 },
            {1, 1, 1, 1, 6, 0, 2, 0, 0, 2, 0, 0, 2, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 2, 0, 0, 2, 0, 0, 2, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 2, 0, 0, 2, 0, 0, 2, 0, 0, 0, 0, 0 },
            {0, 0, 8, 3, 3, 1, 6, 0, 0, 2, 0, 0, 2, 0, 0, 0, 0, 0 },
            {0, 0, 2, 0, 0, 0, 0, 0, 0, 2, 0, 0, 2, 0, 0, 0, 0, 0 },
            {0, 0, 7, 1, 1, 1, 1, 1, 1, 6, 0, 0, 7, 1, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        };

    float timer = 0;

    GameObject heart;
    // Start is called before the first frame update
    void Start()
    {
        heart = GameObject.FindGameObjectWithTag("Heart");

        for (int i = 0; i  < Y; i++)
        {
            GameObject empty = new GameObject("Row: " + i.ToString()) as GameObject;
            empty.transform.parent = this.gameObject.transform;
            for (int j = 0; j < X; j++)
            {
                Vector2 pos = new Vector2(j, (Y - 1) - i);
                GameObject tileObj = Instantiate (tileObject, pos, Quaternion.identity) as GameObject;
                tileObj.transform.parent = empty.gameObject.transform;
                tileObj.name = "Tile: " + j.ToString() + ", " + i.ToString();
                TileBase tb = tileObj.GetComponent<TileBase>();

                if (tb != null && tileObj != null)
                {
                    tb.SetType(tileTypes[Grid[i, j]], j, i);
                }
            }
        }

        this.transform.position = new Vector3(-9.0f, -5.0f, 1.0f);

    }

    // Update is called once per frame
    void Update()
    {
        if (IceHeart.Health <= 0)
        {
            heart.SetActive(false);
            PlayerController pc = GameObject.Find("PlayerInfo").GetComponent<PlayerController>();
            
            pc.healthText.text = "GameOver";
            pc.moneyText.text = "GameOver";
            pc.towerInfoText.text = "Game\nOver\nGame\nOver";
            pc.waveText.text = "";

            timer += Time.deltaTime;

            if (timer > 3)
            {
                pc.towerInfoText.text = "";
                Reset();
            }
        }
    }


    private void Reset()
    {
        IceHeart.Health = 12;
        PlayerController.Money = 350;
        Spawner.CurrentSpawns = 0;
        Spawner.CurrentWave = 0;
        timer = 0;

        heart.SetActive(true);

        GameObject[] enems = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in enems)
        {
            Destroy(obj);
        }

        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject tile in tiles)
        {
            TileBase tb = tile.GetComponent<TileBase>();
            tb.Reset();
        }

    }

}
