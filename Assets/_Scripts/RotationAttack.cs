using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAttack : MonoBehaviour
{
    public float TimeAlive;
    // Start is called before the first frame update
    void Start()
    {
        TimeAlive = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TimeAlive += Time.deltaTime;
        if (TimeAlive > 0.8)
            Destroy(gameObject);
    }
}
