using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsTarget : MonoBehaviour
{
  
    public string targetTag = "Player"; // Tag del objeto al que se mover�
    public float speed = 5f; // Velocidad de movimiento
    public float detectionRange = 10f; // Rango de detecci�n

    private Transform targetTransform;

    void Start()
    {
        // Buscar el objeto con el tag especificado
        GameObject target = GameObject.FindGameObjectWithTag(targetTag);
        if (target != null)
        {
            targetTransform = target.transform;
        }
        else
        {
            Debug.LogWarning("No se encontr� un objeto con el tag " + targetTag);
        }
    }

    void Update()
    {
        if (targetTransform != null)
        {
            // Calcular la distancia al objetivo
            float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);

            // Comprobar si el objetivo est� dentro del rango de detecci�n
            if (distanceToTarget <= detectionRange)
            {
                // Calcular la direcci�n hacia el objetivo
                Vector3 direction = (targetTransform.position - transform.position).normalized;

                // Mover el objeto hacia el objetivo
                transform.position += direction * speed * Time.deltaTime;

                // Opcional: Rotar el objeto para que mire hacia el objetivo
                // Quaternion targetRotation = Quaternion.LookRotation(direction);
                // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
            }
        }
    }
}
