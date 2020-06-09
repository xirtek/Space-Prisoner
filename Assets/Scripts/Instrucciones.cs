using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instrucciones : MonoBehaviour {

    [Header("A Nivel 1")]
    [SerializeField]
    private string nivelACargar;


    void Start()
    {
        
    }

    public void Iniciar()
    {
        SceneManager.LoadScene(nivelACargar);
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}
