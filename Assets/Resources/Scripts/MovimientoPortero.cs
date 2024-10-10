using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPortero : MonoBehaviour
{
    public float velocidad = 5f; 
    public float rangoMovimiento = 5f; 

    private Vector3 posicionInicial;
    private bool moviendoALaDerecha = true;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        IncrementarVelocidad(0.0005f);
       
        float desplazamiento = velocidad * Time.deltaTime;

        // Mover la cápsula de izquierda a derecha
        if (moviendoALaDerecha)
        {
            transform.position += new Vector3(desplazamiento, 0, 0);

            
            if (transform.position.x >= posicionInicial.x + rangoMovimiento)
            {
                moviendoALaDerecha = false;
            }
        }
        else
        {
            transform.position -= new Vector3(desplazamiento, 0, 0);

            
            if (transform.position.x <= posicionInicial.x - rangoMovimiento)
            {
                moviendoALaDerecha = true;
            }
        }
    }

    
    public void IncrementarVelocidad(float incremento)
    {
        velocidad += incremento;
    }
}
