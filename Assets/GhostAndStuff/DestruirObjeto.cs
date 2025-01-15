using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirObjeto : MonoBehaviour
{
  

    // Este método se llama cuando otro collider entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Destruye el objeto que tiene este script adjunto
            Destroy(gameObject);
        }
    }
}

