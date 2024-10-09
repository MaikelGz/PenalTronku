using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowBall : MonoBehaviour
{
    public float FuerzaMaxima = 20f;   // La máxima fuerza que se puede alcanzar
    public Slider sliderPotencia;      // El Slider en la UI
    public Transform Target;           // El objeto Target al que la bola se lanzará
    private float fuerzaActual = 0f;   // Fuerza acumulada mientras se mantiene espacio
    private bool cargando = false;     // Saber si se está cargando la fuerza
    private Vector3 posicionInicial;   // La posición inicial de la bola
    private Rigidbody rb;              // Referencia al Rigidbody de la bola

    void Start()
    {
        // Guardar la posición inicial de la bola al inicio del juego
        posicionInicial = transform.position;
        rb = GetComponent<Rigidbody>(); // Obtener referencia al Rigidbody
    }

    void Update()
    {
        // Si se presiona la tecla de espacio, comenzamos a cargar la fuerza
        if (Input.GetKey(KeyCode.Space))
        {
            cargando = true;
            CargarFuerza();
        }

        // Cuando se suelta la tecla de espacio, lanzamos la bola con la fuerza acumulada
        if (Input.GetKeyUp(KeyCode.Space) && cargando)
        {
            LanzaBolaHaciaTarget();
            ResetearCarga();
            StartCoroutine(TepeDespuésDeTiempo(3f)); // Iniciar la cuenta regresiva para el teletransporte
        }
    }

    void CargarFuerza()
    {
        // Aumentar la fuerza actual, pero sin exceder la máxima fuerza
        fuerzaActual += Time.deltaTime * FuerzaMaxima;
        fuerzaActual = Mathf.Clamp(fuerzaActual, 0, FuerzaMaxima);

        // Actualizar el valor del slider según la fuerza acumulada
        sliderPotencia.value = fuerzaActual / FuerzaMaxima;
    }

    void LanzaBolaHaciaTarget()
    {
        // Calcular la dirección hacia el Target
        Vector3 direccion = (Target.position - transform.position).normalized;

        // Aplicar la fuerza acumulada en la dirección del Target
        rb.AddForce(direccion * fuerzaActual, ForceMode.Impulse);
    }

    IEnumerator TepeDespuésDeTiempo(float tiempo)
    {
        // Esperar el tiempo indicado (3 segundos en este caso)
        yield return new WaitForSeconds(tiempo);

        // Teletransportar la pelota a su posición inicial
        rb.velocity = Vector3.zero;    // Detener cualquier movimiento de la bola
        rb.angularVelocity = Vector3.zero; // Detener la rotación de la bola
        transform.position = posicionInicial; // Teletransportar a la posición inicial
    }

    void ResetearCarga()
    {
        // Resetear la fuerza y el estado de carga
        fuerzaActual = 0f;
        sliderPotencia.value = 0f;
        cargando = false;
    }
}
