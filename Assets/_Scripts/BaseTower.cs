using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    public SpriteRenderer BaseSprite;
    public SpriteRenderer BarrelSprite;

    public Tower tower;

    public float Power;
    public float Range;
    public float ShootDelay;
    public float Timer;

    public GameObject playerController;
    PlayerController pc;

    public Transform target = null;

    public GameObject projectile = null;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("PC");
        pc = playerController.GetComponent<PlayerController>();
        tower = pc.GetTower();

        InvokeRepeating("UpdateTarget", 0.1f, 0.8f);
    }

    void UpdateTarget()
    {
        if (target != null && Vector2.Distance(transform.position, target.position) <= Range)
        {
            //return;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortest = Mathf.Infinity;
        GameObject nearest = null;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);

            if (dist < shortest)
            {
                shortest = dist;
                nearest = enemy;
            }
        }

        if (nearest != null && shortest <= Range)
        {
            target = nearest.transform;
        }
        else
            target = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        Vector2 dir = target.position - transform.position;
        dir.Normalize();
       float z = Mathf.Atan2(dir.y, dir.x);

        BarrelSprite.gameObject.transform.rotation = Quaternion.EulerAngles(0, 0, z - (3.14f /2f));

        Shoot();

    }

    public void Shoot()
    {
        if (target != null)
        {
            Timer += Time.deltaTime;

            if(Timer > ShootDelay)
            {
                GameObject obj = Instantiate(projectile, this.transform.position, Quaternion.EulerAngles(this.transform.eulerAngles));
                obj.transform.parent = this.transform;
                Timer -= ShootDelay;
            }
        }
    }
    float abs(float a)
    {
        if (a < 0)
        { return a + 180f; }

        return a;
    }

    public void SetTower(Tower t)
    {
        tower = t;
        Timer = 0;
        UpdateTower();
    }

    void UpdateTower()
    {
        if (tower == null)
            return;
        else
        {
            
            BaseSprite.sprite = tower.Base;
            BarrelSprite.sprite = tower.Barrel;
            Range = tower.Range;
            Power = tower.Power;
            ShootDelay = tower.TimeDelay;

            projectile = tower.Bullet;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }

}
