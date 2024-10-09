using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de movimiento del Target

    void Update()
    {
        // Movimiento en el plano XZ con las teclas WASD
        float movX = Input.GetAxis("Horizontal"); // Detecta A y D
        float movY = Input.GetAxis("Vertical");   // Detecta W y S

        // Aplicar movimiento
        Vector3 movimiento = new Vector3(movX, movY, 0) * velocidad * Time.deltaTime;
        transform.Translate(movimiento);
    }
}
