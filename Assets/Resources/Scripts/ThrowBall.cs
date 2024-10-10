using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowBall : MonoBehaviour
{
    public float FuerzaMaxima = 20f;   
    public Slider sliderPotencia;     
    public Transform Target;           
    private float fuerzaActual = 0f;   
    private bool cargando = false;     
    private Vector3 posicionInicial;  
    private Rigidbody rb;              

    void Start()
    {
        
        posicionInicial = transform.position;
        rb = GetComponent<Rigidbody>(); 
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.Space))
        {
            cargando = true;
            CargarFuerza();
        }

        
        if (Input.GetKeyUp(KeyCode.Space) && cargando)
        {
            LanzaBolaHaciaTarget();
            ResetearCarga();
            StartCoroutine(TepeDespuésDeTiempo(3f)); 
        }
    }

    void CargarFuerza()
    {
        
        fuerzaActual += Time.deltaTime * FuerzaMaxima;
        fuerzaActual = Mathf.Clamp(fuerzaActual, 0, FuerzaMaxima);

       
        sliderPotencia.value = fuerzaActual / FuerzaMaxima;
    }

    void LanzaBolaHaciaTarget()
    {
       
        Vector3 direccion = (Target.position - transform.position).normalized;

       
        rb.AddForce(direccion * fuerzaActual, ForceMode.Impulse);
    }

    IEnumerator TepeDespuésDeTiempo(float tiempo)
    {
       
        yield return new WaitForSeconds(tiempo);

       
        rb.velocity = Vector3.zero;    
        rb.angularVelocity = Vector3.zero; 
        transform.position = posicionInicial; 
    }

    void ResetearCarga()
    {
    
        fuerzaActual = 0f;
        sliderPotencia.value = 0f;
        cargando = false;
    }
}
