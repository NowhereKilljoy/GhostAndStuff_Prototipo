using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cargar escenas

public class ChangeSceneOnKeyPress : MonoBehaviour
{
    public string sceneName = "NombreDeLaEscena"; // Nombre de la escena a cargar
    public float detectionRadius = 2.0f; // Radio de detección alrededor del NPC
    public KeyCode keyToPress = KeyCode.L; // Tecla que debe presionar el jugador

    private Transform player; // Referencia al jugador
    private bool isPlayerInRange = false; // Estado de si el jugador está en el área de interacción

    void Start()
    {
        // Intentamos encontrar al jugador por la etiqueta "Player"
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Comprobamos si el jugador fue encontrado
        if (player == null)
        {
            Debug.LogError("No se ha encontrado el jugador con la etiqueta 'Player'. Asegúrate de que el jugador esté etiquetado correctamente.");
        }
    }

    void Update()
    {
        // Solo procedemos si el jugador ha sido encontrado
        if (player != null)
        {
            // Verificar si el jugador está dentro del rango de detección
            if (Vector3.Distance(player.position, transform.position) < detectionRadius)
            {
                isPlayerInRange = true;
                // Mostrar mensaje al jugador o activar alguna UI para indicarle que puede presionar la tecla
                Debug.Log("Presiona 'L' para iniciar el diálogo.");
            }
            else
            {
                isPlayerInRange = false;
            }

            // Si el jugador está en el rango y presiona la tecla, cambiar la escena
            if (isPlayerInRange && Input.GetKeyDown(keyToPress))
            {
                // Cargar la escena
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}