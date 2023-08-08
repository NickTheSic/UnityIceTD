using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower")]
public class Tower : ScriptableObject
{
    public float Range;
    public float RangeModifier;

    public float Power;
    public float PowerModifier;

    public float TimeDelay;
    public float TimeDelayModifier;

    private float Timer = 0;

    public float BaseCost;
    public float UpgradeCost;
    public float UpgradeOffset;

    public Sprite Base;
    public Sprite Barrel;

    public GameObject Bullet;

    public State CurrentState;
    public enum State
    {
        Idle = 0,
        Active = 1,
        Firing = 2
    }

    public enum Level
    {
        Zero = 0,
        Upgrade = 1,
        Max = 2
    }

    // Start is called before the first frame update
    void Start()
    {
        Timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentState == State.Idle)
        {

        }

        if (CurrentState == State.Active)
        {

        }



    }

}
