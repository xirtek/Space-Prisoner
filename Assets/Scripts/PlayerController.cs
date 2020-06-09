using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    public float horizontal;
    public float speed = 10f;
    private float vertical;

    //Variables de salto
    private bool jump = false;
    private float jumpForce = 12f; //fuerza del impulso
    private bool suelo = false; //detectar que se está tocando el suelo

    //Variable Animator
    private Animator animator;

    //Variables de Sonido
    private AudioSource audioSrc;
    public AudioClip key_clip;

    //Escalera
    private float climbSpeed;
    private float exitHop = 3f;
    private bool usingLadder = false;
    private bool onLadder = false;
    private PlayerController controller;

    //Variables de posición
    private Vector3 start_position;

    //Variable disparo
    public Transform bulletSpawner;
    public GameObject bulletPrefab;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //para obtener el componente
        audioSrc = GetComponent<AudioSource>(); //para obtener el componente de audio
        start_position = transform.position; //para obtener la posición del personaje
        controller = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        horizontal = Input.GetAxis("Horizontal"); // -1 | 1
        Debug.Log(horizontal);

        vertical = Input.GetAxis("Vertical"); // -1 | 1
        Debug.Log(vertical);

        if (Input.GetKeyDown(KeyCode.X) && suelo == true) //se espera la tecla de x para saltar
        {
            jump = true;
        }

        PlayerShooting();
    }


    void FixedUpdate()
    {

        //se va a modificar la velocidad del rigidbody que tiene el personaje en eje x
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("suelo", suelo);

        if (horizontal > 0.1f && Mathf.Abs(horizontal) > 0.1) //1ra es para posición y 2da es para el delay
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (horizontal < -0.1f && Mathf.Abs(horizontal) > 0.1)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (jump) //si está saltando jump==true
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //es para saltar
            jump = false; //como ya saltó se deja en falso
        }
    }




    private void OnCollisionStay2D(Collision2D objeto) //se ejecuta cuando se detecta colisión
    {
        if (objeto.gameObject.CompareTag("Plataforma"))
        {
            suelo = true;
            animator.SetBool("suelo", true);
        }
    }

    private void OnCollisionExit2D(Collision2D objeto) //se ejecuta cuando el evento anterior deja de ejecutarse
    {
        if (objeto.gameObject.CompareTag("Plataforma"))
        {
            suelo = false;
            animator.SetBool("suelo", false);
        }
    }

    //detectar cuando se colisiona con la moneda
    private void OnTriggerEnter2D(Collider2D objeto)
    {

        if (objeto.gameObject.CompareTag("Llaves"))
        {
            GameManager.instance.agregarLlaves(1); //se agrega moneda al contador 1
            audioSrc.PlayOneShot(key_clip);
            Destroy(objeto.gameObject);
        }

        if (objeto.gameObject.CompareTag("Puerta1") && GameManager.instance.llaves == 7) //si toca puerta y ha recoletado 7 llaves, entonces pasa al nivel 2
        {

            SceneManager.LoadScene("Nivel2");
        }

        if (objeto.gameObject.CompareTag("Puerta2") && GameManager.instance.llaves == 8) //si toca puerta y ha recoletado 8 llaves, entonces pasa al nivel 3
        {

            SceneManager.LoadScene("Nivel3");
        }

        if (objeto.gameObject.CompareTag("Puerta3") && GameManager.instance.llaves == 3) //si toca la nave y ha recoletado 3 llaves, entonces termina el juego
        {

            SceneManager.LoadScene("Final");
        }

        if (objeto.gameObject.CompareTag("ZonaMuerte"))
        {
            //Debug.Log("Me morí :v");
            GameManager.instance.PerderVida(1);
            transform.position = start_position;
        }

        if (objeto.gameObject.CompareTag("Enemigo"))
        {
            //Debug.Log("Me morí :v");
            transform.position = start_position;
        }

        if (objeto.gameObject.CompareTag("savePos"))
        {
            //Guardo posición actual del personaje
            start_position = transform.position;
        }
    }


    //subir escalera
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Escalera"))
        {
            if (vertical != 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, Input.GetAxis("Vertical") * 15f);
                rb.gravityScale = 0;
                onLadder = true;
                controller.usingLadder = onLadder;
            }
            else if (Input.GetAxis("Vertical") == 0 && onLadder)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0); //jugador se queda estacionado y no cae por el gravity scale en 0
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Escalera") && onLadder)
        {
            rb.gravityScale = 1.5f;
            onLadder = false;
            controller.usingLadder = onLadder;
        }
    }

    public void PlayerShooting()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
        }

    }


}
