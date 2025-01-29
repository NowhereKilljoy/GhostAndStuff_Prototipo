using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarObjetos : MonoBehaviour
{
   
    // Referencia a los objetos que quieres activar
    public GameObject[] objetosParaActivar;

    // Este método se llama cuando otro collider entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Recorre el array de objetos y activa cada uno
            foreach (GameObject obj in objetosParaActivar)
            {
                obj.SetActive(true);
            }
        }
    }
}

