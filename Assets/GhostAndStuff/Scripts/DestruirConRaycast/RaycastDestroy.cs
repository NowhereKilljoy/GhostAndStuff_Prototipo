using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDestroy : MonoBehaviour


{
    void Update()
    {
        // Si el usuario presiona el bot�n izquierdo del mouse
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            // Crear un rayo desde la c�mara hacia la posici�n del mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Si el rayo colisiona con algo
            if (Physics.Raycast(ray, out hit))
            {
                // Verifica si el objeto tocado es este
                if (hit.collider.gameObject == gameObject)
                {
                    Destroy(gameObject); // Elimina este objeto
                }
            }
        }
    }
}