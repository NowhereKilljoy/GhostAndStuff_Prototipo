using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 10f;  // Rango de detección del enemigo
    public float moveSpeed = 3.5f;     // Velocidad de movimiento del enemigo
    public Transform player;            // Referencia al jugador
    public NavMeshAgent agent;          // Referencia al NavMeshAgent

    private void Start()
    {
        // Obtenemos la referencia al NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Comprobamos la distancia entre el enemigo y el jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Si el jugador está dentro del rango de detección
        if (distanceToPlayer <= detectionRange)
        {
            // Hacemos que el enemigo se mueva hacia el jugador
            agent.SetDestination(player.position);

            // Cambiamos la velocidad de movimiento
            agent.speed = moveSpeed;
        }
        else
        {
            // Si el jugador está fuera del rango de detección, el enemigo no se mueve
            agent.ResetPath();
        }
    }
}