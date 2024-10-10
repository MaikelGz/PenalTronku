using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  
using UnityEngine.SceneManagement; 

public class GolManager : MonoBehaviour
{
    public Text textoGoles;          
    private int cantidadGoles = 0;    
    private bool puedeDetectarGol = true; 
    public float tiempoEspera = 2f;   

    public AudioClip sonidoGol;      
    private AudioSource audioSource; 


    void Start()
    {
       
        audioSource = GetComponent<AudioSource>();

    }
    void OnTriggerEnter(Collider other)
    {
        
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
            
            cantidadGoles++;

           
            textoGoles.text = "Goles: " + cantidadGoles;
            
            
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
        
        puedeDetectarGol = false;   
        yield return new WaitForSeconds(tiempoEspera);
        puedeDetectarGol = true;
    }
}
