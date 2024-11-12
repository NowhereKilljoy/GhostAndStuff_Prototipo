using UnityEngine;

public class EyeLookAtPlayer : MonoBehaviour
{
    public Transform player;  // El transform del jugador
    public float lookInterval = 2f;  // Intervalo de tiempo en segundos entre cada rotación
    private float timer;  // Temporizador para controlar el intervalo

    void Start()
    {
        // Aseguramos que el jugador está asignado correctamente
        if (player == null)
        {
            Debug.LogError("El jugador no está asignado en el inspector");
        }
    }

    void Update()
    {
        // Incrementamos el temporizador
        timer += Time.deltaTime;

        // Si ha pasado el intervalo de tiempo, rotamos el ojo hacia el jugador
        if (timer >= lookInterval)
        {
            // Rotar el ojo hacia la posición del jugador
            transform.LookAt(player);

            // Reiniciar el temporizador
            timer = 0f;
        }
    }
}