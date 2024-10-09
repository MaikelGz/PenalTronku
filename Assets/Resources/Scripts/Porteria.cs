using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverCapsula : MonoBehaviour
{
    public float velocidad = 3f;       // Velocidad de movimiento
    public float limiteIzquierdo = -5f; // Límite izquierdo del movimiento
    public float limiteDerecho = 5f;   // Límite derecho del movimiento

    private bool moviendoDerecha = true; // Controla la dirección del movimiento

    void Update()
    {
        MoverEnBucle();
    }

    void MoverEnBucle()
    {
        // Mover la cápsula de izquierda a derecha
        if (moviendoDerecha)
        {
            transform.position += Vector3.right * velocidad * Time.deltaTime;

            // Si la cápsula alcanza el límite derecho, cambiar de dirección
            if (transform.position.x >= limiteDerecho)
            {
                moviendoDerecha = false;
            }
        }
        else
        {
            transform.position += Vector3.left * velocidad * Time.deltaTime;

            // Si la cápsula alcanza el límite izquierdo, cambiar de dirección
            if (transform.position.x <= limiteIzquierdo)
            {
                moviendoDerecha = true;
            }
        }
    }
}
