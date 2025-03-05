using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcMove : MonoBehaviour
{
    private enum State { Patrullando, Escapando }
    private State currentState = State.Patrullando;

    private NavMeshAgent agente;
    public Transform player;

    public float distanciaEscape = 10f;
    public float rango = 10f;
    public float detectionRange = 5f;
    public float tiempoEspera = 2f;
    public float idle = 0.5f;

    private bool esperando;
    private bool escapando = false;

    private float escapeStartTime;
    private float escapeTimeout = 3f; // Si después de 3s no se mueve, vuelve a patrullar

    private void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        moverAlPunto();
    }

    private void Update()
    {
        float distanciaToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Patrullando:
                Patrol();
                if (distanciaToPlayer < detectionRange)
                {
                    Debug.Log("Detectado jugador, cambiando a ESCAPANDO");
                    currentState = State.Escapando;
                    escapando = false;
                }
                break;

            case State.Escapando:
                if (!escapando)
                {
                    Escape();
                    escapeStartTime = Time.time;
                }

                // Si después de escapeTimeout segundos sigue sin moverse, forzamos patrullaje
                if (Time.time - escapeStartTime > escapeTimeout)
                {
                    Debug.Log("Tiempo de escape agotado, volviendo a patrullar");
                    currentState = State.Patrullando;
                    moverAlPunto();
                }

                if (distanciaToPlayer > detectionRange * 1.5f)
                {
                    Debug.Log("Jugador lejos, volviendo a patrullar");
                    currentState = State.Patrullando;
                    moverAlPunto();
                }
                break;
        }
    }

    private void Patrol()
    {
        if (!esperando && !agente.pathPending && agente.remainingDistance <= agente.stoppingDistance)
        {
            if (Random.value < idle)
            {
                StartCoroutine(esperaMueve());
            }
            else
            {
                moverAlPunto();
            }
        }
    }

    private void moverAlPunto()
    {
        Vector3 randomDirection = Random.insideUnitSphere * rango;
        randomDirection += transform.position;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDirection, out hit, rango, NavMesh.AllAreas))
        {
            Debug.Log("Nuevo punto de patrullaje: " + hit.position);
            agente.SetDestination(hit.position);
        }
    }

    IEnumerator esperaMueve()
    {
        esperando = true;
        yield return new WaitForSeconds(tiempoEspera);
        moverAlPunto();
        esperando = false;
    }

    private void Escape()
    {
        escapando = true;

        // Calculamos el punto de escape sin usar NavMesh.SamplePosition
        Vector3 escapeDirection = (transform.position - player.position).normalized;
        Vector3 escapeTarget = transform.position + escapeDirection * distanciaEscape;

        // Verificamos si ese punto está dentro del NavMesh antes de aplicarlo
        if (agente.isOnNavMesh) // Verifica si el NPC está sobre el NavMesh antes de moverse
        {
            Debug.Log("Escapando sin SamplePosition a: " + escapeTarget);
            agente.SetDestination(escapeTarget);
        }
        else
        {
            Debug.LogWarning("NPC fuera del NavMesh, buscando un punto válido...");
            NavMeshHit hit;
            if (NavMesh.SamplePosition(escapeTarget, out hit, distanciaEscape, NavMesh.AllAreas))
            {
                Debug.Log("Punto de escape encontrado con SamplePosition: " + hit.position);
                agente.SetDestination(hit.position);
            }
            else
            {
                Debug.LogError("No se encontró un punto de escape en el NavMesh. Volviendo a patrullar.");
                currentState = State.Patrullando;
                moverAlPunto();
            }
        }
    }
}