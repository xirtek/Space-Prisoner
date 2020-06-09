using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject startPoint;
    public GameObject endPoint;

    public float enemySpeed = 5f;
    private bool isGoingRight;

    public int enemyHealth = 100; //vida

    void Start()
    {
        if (isGoingRight)
        {
            transform.position = startPoint.transform.position;
        }
        else
        {
            transform.position = endPoint.transform.position;
        }
    }

    void Update()
    {
        if (!isGoingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint.transform.position, enemySpeed * Time.deltaTime);

            if (transform.position == endPoint.transform.position)
            {
                isGoingRight = true;
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        if (isGoingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPoint.transform.position, enemySpeed * Time.deltaTime);

            if (transform.position == startPoint.transform.position)
            {
                isGoingRight = false;
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(startPoint.transform.position, endPoint.transform.position);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Bullet")
        {
            enemyHealth -= BulletMovement.damage;
            if (enemyHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
