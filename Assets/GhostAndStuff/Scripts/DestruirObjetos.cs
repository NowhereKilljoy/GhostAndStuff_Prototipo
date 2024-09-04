using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirObjetos : MonoBehaviour
{
   
    
       
    // Método que se llama cuando ocurre un trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto con el que colisiona tiene el tag "bullet"
        if (other.CompareTag("BalaFantasmal"))
        {
            // Destruye el objeto actual
            Destroy(gameObject);
        }
    }
}

