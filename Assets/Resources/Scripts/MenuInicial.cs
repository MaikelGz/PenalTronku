using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cargar escenas

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

    // Método que se llama cuando el botón "Salir" es presionado
    public void SalirJuego()
    {
        // Cierra el juego. Funciona cuando se ejecuta como una aplicación independiente
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}
