using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "Spot Tile")]
public class Tile : ScriptableObject
{
    public Sprite tile;

    public Type State;
    public enum Type
    {
        Taken = -1,
        Placeable = 0,
        Left,
        Down,
        Right,
        Up,
        TurnLeft,
        TurnRight
    }

}
