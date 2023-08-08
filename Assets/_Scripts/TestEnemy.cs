using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{

    public float speed;
    public float health;

    public Transform[] pathPoints;
    public int currentPath;

    Vector3 scaleNormal;
    Vector3 scaleInverse;

    bool facingRight;

    public float distanceTraveled; //So the towers aim at the first tower;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(0.4f + (Spawner.CurrentWave / 90), 0.5f + (Spawner.CurrentWave / 70));
        health = 40 + (Spawner.CurrentWave * 5 + (Spawner.CurrentWave - 1));
        pathPoints = Waypoints.points;
        Reset();
    }

    private void Reset()
    {
        currentPath = 0;
        facingRight = true;
        distanceTraveled = 0;

        scaleNormal = transform.localScale;
        scaleInverse = scaleNormal;
        scaleInverse.x *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
            Spawner.CurrentSpawns--;
            PlayerController.Money += Random.Range(5 , 6 + Spawner.CurrentWave / 2);
            return;
        }
        distanceTraveled += speed * Time.deltaTime;
        Vector2 pathPos = pathPoints[currentPath].transform.position;

        transform.position = Vector2.MoveTowards(transform.position, pathPos, speed * Time.deltaTime);

        float dist = Vector2.Distance(transform.position, pathPos);
 
        if (dist < 0.1f)
        {
            currentPath++;
            
            if (currentPath == pathPoints.Length)
            {
                Destroy(this.gameObject);
                Spawner.CurrentSpawns--;
                return;
            }

            float newPathX = pathPoints[currentPath].transform.position.x;
            if (newPathX < transform.position.x && facingRight)
            {
                transform.localScale = scaleInverse;
                facingRight = !facingRight;
            }

            if (newPathX > transform.position.x && !facingRight )
            {
                transform.localScale = scaleNormal;
                facingRight = !facingRight;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Heart")
        {
            Spawner.CurrentSpawns--;
            Destroy(this.gameObject);
            IceHeart.Health--;
        }

        if (collision.gameObject.tag == "Bullet")
        {
            health -= 5;
        }
    }

}
