using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NpcMove : MonoBehaviour
{
    private enum State { Patrullando, Escapando }
    private State currentState = State.Patrullando;

    private NavMeshAgent agente;
    private Animator animator;
    private AudioSource audioSource;

    public Transform player;

    public float distanciaEscape = 10f;
    public float rango = 10f;
    public float detectionRange = 5f;
    public float tiempoEspera = 2f;
    public float idle = 0.5f;

    private bool esperando = false;
    private bool escapando = false;

    private float escapeStartTime;
    private float escapeTimeout = 3f;

    private void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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

                if (Time.time - escapeStartTime > escapeTimeout)
                {
                    currentState = State.Patrullando;
                    moverAlPunto();
                }

                if (distanciaToPlayer > detectionRange * 1.5f)
                {
                    currentState = State.Patrullando;
                    moverAlPunto();
                }
                break;
        }

        UpdateAnimatorAndAudio();
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
        Vector3 escapeDirection = (transform.position - player.position).normalized;
        Vector3 escapeTarget = transform.position + escapeDirection * distanciaEscape;

        if (agente.isOnNavMesh)
        {
            agente.SetDestination(escapeTarget);
        }
        else
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(escapeTarget, out hit, distanciaEscape, NavMesh.AllAreas))
            {
                agente.SetDestination(hit.position);
            }
            else
            {
                currentState = State.Patrullando;
                moverAlPunto();
            }
        }
    }

    private void UpdateAnimatorAndAudio()
    {
        if (animator == null || audioSource == null) return;

        float velocidad = agente.velocity.magnitude;
        bool estaEscapando = velocidad > 0.1f;

        animator.SetBool("isEscaping", estaEscapando);
        animator.SetBool("isIdle", !estaEscapando);

        if (estaEscapando && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (!estaEscapando && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}

