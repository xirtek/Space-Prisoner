using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalManager : MonoBehaviour
{
    [Header("A Menu")]
    [SerializeField]
    private string nivelACargar;


    void Start()
    {

    }

    public void VolveraJugar()
    {
        SceneManager.LoadScene(nivelACargar);
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }

}
