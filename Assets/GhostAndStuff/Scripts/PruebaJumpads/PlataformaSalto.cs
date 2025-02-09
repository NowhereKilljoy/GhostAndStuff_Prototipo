using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaSalto : MonoBehaviour
{
    public float fuerzaDeSalto = 10f; // Fuerza con la que el jugador será impulsado hacia arriba

    // Esta función se llama cuando otro collider entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto que entra en el trigger tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Asegurarnos de que el jugador tiene un Rigidbody
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Aplicamos una fuerza hacia arriba
                rb.velocity = new Vector3(rb.velocity.x, fuerzaDeSalto, rb.velocity.z);
            }
        }
    }
}