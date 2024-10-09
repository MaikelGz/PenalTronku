using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPortero : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad del movimiento
    public float rangoMovimiento = 5f; // Distancia máxima de movimiento en cada dirección

    private Vector3 posicionInicial;
    private bool moviendoALaDerecha = true;

    void Start()
    {
        // Guardar la posición inicial de la cápsula
        posicionInicial = transform.position;
    }

    void Update()
    {
        IncrementarVelocidad(0.0005f);
        // Calcular el desplazamiento en función de la velocidad y el tiempo
        float desplazamiento = velocidad * Time.deltaTime;

        // Mover la cápsula de izquierda a derecha
        if (moviendoALaDerecha)
        {
            transform.position += new Vector3(desplazamiento, 0, 0);

            // Si alcanza el límite derecho, cambiar dirección
            if (transform.position.x >= posicionInicial.x + rangoMovimiento)
            {
                moviendoALaDerecha = false;
            }
        }
        else
        {
            transform.position -= new Vector3(desplazamiento, 0, 0);

            // Si alcanza el límite izquierdo, cambiar dirección
            if (transform.position.x <= posicionInicial.x - rangoMovimiento)
            {
                moviendoALaDerecha = true;
            }
        }
    }

    // Método público para incrementar la velocidad de la cápsula
    public void IncrementarVelocidad(float incremento)
    {
        velocidad += incremento;
    }
}
