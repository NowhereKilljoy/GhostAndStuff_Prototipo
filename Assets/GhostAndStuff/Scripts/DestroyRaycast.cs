using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruiRaycast : MonoBehaviour
{
    public float distanciaRaycast = 200f;

    void Update()
    {
        // Realiza un raycast hacia adelante
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, distanciaRaycast))
        {
            // Verifica si el objeto impactado tiene el tag "Rompible"
            if (hit.collider.CompareTag("Rompible"))
            {
                // Destruye el objeto que fue impactado
                Destroy(hit.collider.gameObject);
            }
        }
    }

    // Opcional: para visualizar el raycast en el editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * distanciaRaycast);
    }
}



