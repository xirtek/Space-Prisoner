using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("A Instrucciones")]
    [SerializeField]
    private string nivelACargar;


    void Start()
    {
        PlayerPrefs.DeleteKey("Vidas");
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
