using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuInicial : MonoBehaviour
{

    public void JugarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Repetir()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    
    public void SalirJuego()
    {
        
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}
