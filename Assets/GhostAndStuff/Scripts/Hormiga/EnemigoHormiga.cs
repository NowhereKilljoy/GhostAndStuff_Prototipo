using System.Collections.Generic;
using UnityEngine;

public class EnemigoHormiga : MonoBehaviour
{
    public string targetTag = "Player";
    public float speedH = 5f;
    public float detectionRangeH = 10f;
    public float patrolSpeedH = 2f;
    // public float knockbackDistanceH = 1f;
    //  public float knockbackDurationH = 0.5f;
    public float maxScapeHeightDifferenceH = 3f;
    public float uprightSpeedH = 2f;
    // public float patrolDistanceH = 5f; // Distancia fija de patrullaje en una dirección
    public List<Transform> patrolPoints; // Lista de puntos de patrullaje
    public float arrivalThreshold = 0.5f; // Distancia mínima para considerar que ha llegado a un punto

    private Transform targetTransform;
    private Transform currentPatrolPoint;
    // private Vector3 patrolStartPosition;
    private bool IsScapingPlayer = false;
    private bool isKnockedBack = false;
    //  private Vector3 knockbackDirection;
    //  private float knockbackTimer;
    //  private float traveledDistance = 0f; // Distancia recorrida en la patrulla
    //  private int patrolDirection = 1; // 1 para adelante, -1 para regresar
    //  private bool shouldTurnAround = false; // Bandera para controlar el giro

    void Start()
    {
        GameObject target = GameObject.FindGameObjectWithTag(targetTag);
        if (target != null)
        {
            targetTransform = target.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró un objeto con el tag " + targetTag);
        }

        ChooseRandomPatrolPoint();

    }

    void Update()
    {
        CorrectOrientation(); // Asegurar que esté "de pie"

        if (IsScapingPlayer && targetTransform != null)
        {
            Scape();
        }
        else
        {
            Patrol();
        }

        float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);
        float heightDifference = Mathf.Abs(transform.position.y - targetTransform.position.y);

        if (distanceToTarget <= detectionRangeH && heightDifference <= maxScapeHeightDifferenceH)
        {
            IsScapingPlayer = true;
        }
        else
        {
            IsScapingPlayer = false;
        }
    }

    // Función para corregir la orientación y asegurar que el enemigo siempre esté de pie
    private void CorrectOrientation()
    {
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.x = Mathf.LerpAngle(currentRotation.x, 0f, Time.deltaTime * uprightSpeedH);
        currentRotation.z = Mathf.LerpAngle(currentRotation.z, 0f, Time.deltaTime * uprightSpeedH);
        transform.eulerAngles = currentRotation;
    }

    // Función de patrullaje en línea recta
    private void Patrol()
    {
        if (currentPatrolPoint == null || isKnockedBack) return;

        // Mover hacia el punto de patrullaje actual
        Vector3 direction = (currentPatrolPoint.position - transform.position).normalized;
        transform.position += direction * speedH * Time.deltaTime;

        // Girar hacia la dirección del patrullaje
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speedH);

        // Verificar si ha llegado al punto de patrullaje actual
        if (Vector3.Distance(transform.position, currentPatrolPoint.position) < arrivalThreshold)
        {
            ChooseRandomPatrolPoint(); // Elegir un nuevo punto de patrullaje al azar
        }
    }

    private void ChooseRandomPatrolPoint()
    {
        if (patrolPoints.Count == 0) return;

        // Elegir un nuevo punto aleatorio que no sea el actual
        Transform newPatrolPoint;
        do
        {
            newPatrolPoint = patrolPoints[Random.Range(0, patrolPoints.Count)];
        }
        while (newPatrolPoint == currentPatrolPoint);

        currentPatrolPoint = newPatrolPoint;
        Debug.Log("Nuevo punto de patrullaje elegido: " + currentPatrolPoint.name);
    }



    private void Scape()
    {
        Vector3 direction = (transform.position - targetTransform.position).normalized; // Dirección opuesta al jugador
        transform.position += direction * speedH * Time.deltaTime; // Mover en dirección opuesta

        Quaternion targetRotation = Quaternion.LookRotation(direction); // Rotar en la dirección de escape
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speedH);
    }
}
