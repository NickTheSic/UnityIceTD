using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRegular : MonoBehaviour
{
    public float speed = 10;
    public Transform target;
    public float aliveTimer;
    // Start is called before the first frame update
    void Start()
    {
        target = GetComponentInParent<BaseTower>().target;
        aliveTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        aliveTimer += Time.deltaTime;
        if (aliveTimer > 1.5f)
            Destroy(this.gameObject);

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }

}
