using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceHeart : MonoBehaviour
{
    public Sprite[] spriteLevels;
    public SpriteRenderer Spr;

    public static int Health = 12;
    int Modifier; 

    public float speed = 0.25f;
    public float highest = -3.5f;
    public float lowest = -4.5f;
    public bool movingUp;
    public float x;

    // Start is called before the first frame update
    void Start()
    {

        if (Health != 0)
            Modifier = Health / 6;

        x = this.transform.position.x;

        movingUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        float y = this.transform.position.y;

        if (movingUp)
        {
            y += speed * Time.deltaTime;
        }
        else
        {
            y -= speed * Time.deltaTime;
        }

        if (y > highest)
            movingUp = false;

        if (y < lowest)
            movingUp = true;

        this.transform.position = new Vector3(x, y, -1);


        if (Health <= 0)
        {
            Health = 0;
        }

        if (Health == 12)
            Spr.sprite = spriteLevels[5];
        else
            Spr.sprite = spriteLevels[Health / Modifier];

    }

    private void OnMouseDown()
    {
        Health--;
        //Debug.Log(Health.ToString());
        //Debug.Log((Health / Modifier).ToString());

        if (Health <= 0)
        {
            Health = 0;
        }

        Spr.sprite = spriteLevels[Health / Modifier];
    }

}
