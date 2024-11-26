using UnityEngine;

public class Puerta : MonoBehaviour
{
    public Transform door; // La puerta que se moverá
    public bool isUnlocked = false; // Indica si la puerta está desbloqueada

    public Transform openTransform;
    public Transform closedTransform; 

    public float speed = 2.0f; // Velocidad de apertura/cierre

    private bool playerInTrigger = false;
    private bool isMoving = false;
    private Vector3 targetPosition; 

    private void Start()
    {
        door.position = closedTransform.position; // Comienza en la posición cerrada
        targetPosition = closedTransform.position;
    }

    private void Update()
    {
        // Mover la puerta hacia la posición objetivo
        if (isMoving)
        {
            door.position = Vector3.Lerp(door.position, targetPosition, Time.deltaTime * speed);

            // Detener el movimiento cuando llegue a la posición objetivo
            if (Vector3.Distance(door.position, targetPosition) < 0.01f)
            {
                door.position = targetPosition;
                isMoving = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isUnlocked)
        {
            playerInTrigger = true;
            targetPosition = openTransform.position; // Abrir la puerta
            isMoving = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            targetPosition = closedTransform.position; // Cerrar la puerta
            isMoving = true;
        }
    }
}
