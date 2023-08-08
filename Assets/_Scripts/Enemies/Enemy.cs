using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy Type")]
public class Enemy : ScriptableObject
{

    public Sprite sprite;

    public float health;
    public float speed;

    public float goldAmount;
}
