using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera mainCamera; // Cámara principal
    public CinemachineVirtualCamera actionCamera; // Cámara de la acción (balón)
    public float cameraSwitchTime = 10f; // Tiempo que la segunda cámara estará activa

    // Método para lanzar el balón y cambiar la cámara
    public void LaunchBallAndSwitchCamera()
    {
        // Cambia la prioridad de la cámara de la acción para que sea la vista activa
        actionCamera.Priority = 11;
        mainCamera.Priority = 5;

        // Inicia el cambio de cámara temporal
        StartCoroutine(SwitchBackToMainCamera());
    }

    private IEnumerator SwitchBackToMainCamera()
    {
        // Espera el tiempo definido antes de volver a la cámara principal
        yield return new WaitForSeconds(cameraSwitchTime);

        // Vuelve a darle prioridad a la cámara principal
        mainCamera.Priority = 11;
        actionCamera.Priority = 5;
    }
}

