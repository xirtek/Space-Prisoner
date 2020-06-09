using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;


    //Variables llaves
    public int llaves = 0; //se verá en unity la variable
    public Text llaveUI;


    //Variables de pausa
    private GameObject[] pauseObjects;
    private bool isPaused = false;

    //Variable vida personaje
    private int vida = 3;


    //se ejecuta antes del start, una vez que se apreta Play en unity y luego se ejecuta Start
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject); //destruye gameObject para que al pasar de escena no se repitan y así queda el de la 1ra escena
        }

    }


    // Use this for initialization
    void Start()
    {
        vida = PlayerPrefs.GetInt("Vidas", 3);
        ActualizarHudVida();
        llaveUI.text = this.llaves.ToString();
        pauseObjects = GameObject.FindGameObjectsWithTag("PausaElement"); //retorna elementos que tengan el tag PausaElement
        hidePaused(); //para ocultar el panel de pausa

    }

    // Update is called once per frame
    void Update()
    {

        //condicional para detectar si se presiona una tecla
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }

    }


    //función para contar llaves
    public void agregarLlaves(int cantidad)
    {
        this.llaves = this.llaves + cantidad;
        llaveUI.text = this.llaves.ToString();
    }


    //función para ocultar panel de pausa
    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    //función para mostrar la pausa en ventana
    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }


    //función para verificar si está o no en pausa
    public void Pause()
    {
        if (isPaused == true) //si está en pausa
        {
            hidePaused();
            Time.timeScale = 1;
            isPaused = false;

        }
        else //si no está en pausa
        {
            showPaused();
            Time.timeScale = 0;
            isPaused = true;

        }
    }

    public void ActualizarHudVida()
    {
        GameObject.Find("PlayerUI").GetComponent<Text>().text = vida.ToString();
    }

    public void PerderVida(int cantidad)
    {
        if (vida > 0)
        {
            PlayerPrefs.SetInt("Vidas", this.vida - cantidad);
            this.vida = PlayerPrefs.GetInt("Vidas", 3);
            GameObject.Find("PlayerUI").GetComponent<Text>().text = vida.ToString();
        }
        else
        {
            SceneManager.LoadScene("GameOver");
            Awake();
        }
    }

}
