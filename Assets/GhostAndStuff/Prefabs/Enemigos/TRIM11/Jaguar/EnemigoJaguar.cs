using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemigoJaguar : MonoBehaviour
{
    private Transform objetivo;
    private NavMeshAgent agente;
    private Rigidbody rb;

    public float velocidadPatrulla = 2f;
    public float velocidadPersecucion = 5f;
    public float velocidadEscape = 6f;
    public float fuerzaEmbestida = 20f;
    public float fuerzaKnockback = 3f;
    public float rangoDeteccion = 10f;
    public float rangoEmbestida = 5f;
    public float rangoEscape = 15f;
    public float duracionDash = 0.5f;
    public float duracionAturdimiento = 1f;
    public float duracionEscape = 30f;
    public int contadorKnockbacks = 0;
    private bool enEscape = false;

    private bool realizandoDash = false;
    private bool aturdido = false;
    public Transform[] puntosPatrulla;
    private int indicePatrullaActual = 0;

    void Start()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
            objetivo = jugador.transform;

        agente = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        if (aturdido || realizandoDash) return;

        if (enEscape)
        {
            Escapar();
            return;
        }

        float distancia = Vector3.Distance(transform.position, objetivo.position);
        if (contadorKnockbacks > 4)
        {
            IniciarEscape();
            return;
        }

        if (distancia <= rangoEmbestida)
        {
            IniciarEmbestida();
            return;
        }
        if (distancia <= rangoDeteccion)
        {
            Perseguir();
            return;
        }
        Patrullar();
    }

    private void Patrullar()
    {
        if (puntosPatrulla.Length == 0) return;

        Transform puntoDestino = puntosPatrulla[indicePatrullaActual];
        agente.speed = velocidadPatrulla;
        agente.SetDestination(puntoDestino.position);

        if (Vector3.Distance(transform.position, puntoDestino.position) < 0.5f)
            indicePatrullaActual = (indicePatrullaActual + 1) % puntosPatrulla.Length;
    }

    private void Perseguir()
    {
        if (objetivo != null)
        {
            agente.speed = velocidadPersecucion;
            agente.SetDestination(objetivo.position);
        }
    }

    private void IniciarEmbestida()
    {
        realizandoDash = true;
        agente.enabled = false;
        Vector3 direccion = (objetivo.position - transform.position).normalized;
        direccion.y = 0;
        rb.velocity = Vector3.zero;
        rb.AddForce(direccion * fuerzaEmbestida, ForceMode.Impulse);
        Invoke("FinalizarEmbestida", duracionDash);
    }

    private void FinalizarEmbestida()
    {
        realizandoDash = false;
        agente.enabled = true;
        rb.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AplicarKnockback(collision);
        }
    }

    private void AplicarKnockback(Collision collision)
    {
        aturdido = true;
        contadorKnockbacks++;
        agente.enabled = false;
        Vector3 direccion = (transform.position - collision.transform.position).normalized;
        direccion.y = 0;
        rb.velocity = Vector3.zero;
        rb.AddForce(direccion * fuerzaKnockback, ForceMode.Impulse);
        Invoke("ReactivarNavMesh", duracionAturdimiento);
    }

    private void ReactivarNavMesh()
    {
        agente.enabled = true;
        aturdido = false;
        rb.velocity = Vector3.zero;
    }

    private void IniciarEscape()
    {
        enEscape = true;
        contadorKnockbacks = 0;
        agente.speed = velocidadEscape;
        Vector3 direccionEscape = (transform.position - objetivo.position).normalized;
        agente.SetDestination(transform.position + direccionEscape * rangoEscape);
        Invoke("FinalizarEscape", duracionEscape);
    }

    private void Escapar()
    {
        if (Vector3.Distance(transform.position, objetivo.position) > rangoEscape)
        {
            Transform puntoMasCercano = ObtenerPuntoPatrullaMasCercano();
            transform.position = puntoMasCercano.position;
            enEscape = false;
            return;
        }
    }

    private void FinalizarEscape()
    {
        Transform puntoMasLejano = ObtenerPuntoPatrullaMasLejano();
        transform.position = puntoMasLejano.position;
        enEscape = false;
    }

    private Transform ObtenerPuntoPatrullaMasCercano()
    {
        Transform puntoMasCercano = null;
        float distanciaMinima = Mathf.Infinity;
        foreach (Transform punto in puntosPatrulla)
        {
            float distancia = Vector3.Distance(objetivo.position, punto.position);
            if (distancia < distanciaMinima)
            {
                distanciaMinima = distancia;
                puntoMasCercano = punto;
            }
        }
        return puntoMasCercano;
    }

    private Transform ObtenerPuntoPatrullaMasLejano()
    {
        Transform puntoMasLejano = null;
        float distanciaMaxima = 0;
        foreach (Transform punto in puntosPatrulla)
        {
            float distancia = Vector3.Distance(objetivo.position, punto.position);
            if (distancia > distanciaMaxima)
            {
                distanciaMaxima = distancia;
                puntoMasLejano = punto;
            }
        }
        return puntoMasLejano;
    }
}