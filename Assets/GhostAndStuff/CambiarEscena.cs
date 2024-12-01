using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Necesario para cargar escenas

public class CambiarEscena : MonoBehaviour
{
    // Nombre de la escena a la que se cambiará
    public string nombreDeLaEscena;

    // Detecta cuando el jugador entra en el collider
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Cambia a la escena especificada
            SceneManager.LoadScene(nombreDeLaEscena);
        }
    }
}