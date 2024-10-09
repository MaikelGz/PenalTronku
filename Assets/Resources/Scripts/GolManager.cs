using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Necesario para manejar la UI
using UnityEngine.SceneManagement; // Necesario para cargar escenas

public class GolManager : MonoBehaviour
{
    public Text textoGoles;          // Referencia al Texto en el Canvas
    private int cantidadGoles = 0;    // Variable para almacenar el número de goles
    private bool puedeDetectarGol = true; // Controla si es posible detectar un gol
    public float tiempoEspera = 2f;   // Tiempo de espera entre detecciones de goles

    public AudioClip sonidoGol;      // Sonido a reproducir cuando haya un gol
    private AudioSource audioSource; // Fuente de audio


    void Start()
    {
        // Obtener el componente AudioSource del objeto
        audioSource = GetComponent<AudioSource>();

    }
    void OnTriggerEnter(Collider other)
    {
        // Detectar si la pelota traspasa el detector de gol y si es posible detectar el gol
        if (other.CompareTag("Pelota") && puedeDetectarGol)
        {
            SumarGol();
        }
    }

    void SumarGol()
    {
        if(cantidadGoles==9){
            cantidadGoles=0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }else{
            // Incrementar la cantidad de goles
            cantidadGoles++;

            // Actualizar el texto en el Canvas con la cantidad de goles
            textoGoles.text = "Goles: " + cantidadGoles;
            
            // Reproducir el sonido del gol
            if (sonidoGol != null && audioSource != null)
            {
                audioSource.PlayOneShot(sonidoGol);
            }


            

            // Iniciar cooldown para evitar múltiples detecciones
            StartCoroutine(CooldownDeteccion());
        }
        
    }

    IEnumerator CooldownDeteccion()
    {
        // Desactivar la detección de goles temporalmente
        puedeDetectarGol = false;

        // Esperar el tiempo de espera antes de permitir otra detección
        yield return new WaitForSeconds(tiempoEspera);

        // Volver a activar la detección de goles
        puedeDetectarGol = true;
    }
}
