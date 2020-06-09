using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    public GameObject player;
    private Transform playerTrans;

    private Rigidbody2D bulletRB;
    public float bulletSpeed = 20f;
    public float bulletLife = 1f;

    public static int damage;
    public int damageRef = 50; //daño

    //se llama siempre primero que el start
    void Awake()
    {
        damage = damageRef; //para mostrar el daño
        bulletRB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Jugador");
        playerTrans = player.transform;
    }

    // Use this for initialization
    void Start()
    {
        if (playerTrans.localScale.x > 0)
        {
            bulletRB.velocity = new Vector2(bulletSpeed, bulletRB.velocity.y);
            transform.localScale = new Vector3(1, 1, 1); //para que la bala mire hacia izquieda o derecha (para futuros proyectos)
        }
        else
        {
            bulletRB.velocity = new Vector2(-bulletSpeed, bulletRB.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, bulletLife);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Plataforma" || col.tag == "Enemigo")
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }

}
