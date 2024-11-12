using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public Transform[] respawnPoints;  // Lista de puntos de respawn en el escenario
    public GameObject[] respawnIndicators;
    private Transform currentRespawnPoint;  // Punto de respawn actual 


    public static RespawnManager instance;

    void Start()
    {
        if (respawnPoints.Length > 0)
        {
            currentRespawnPoint = respawnPoints[0];  // Inicializar en el primer punto de respawn
        }
    }

    // Método para actualizar el punto de respawn al último checkpoint alcanzado
    public void UpdateRespawnPoint(Transform newRespawnPoint)
    {
        currentRespawnPoint = newRespawnPoint;
        Debug.Log("Punto de respawn actualizado a: " + newRespawnPoint.name);
    }

    // Método para reposicionar al jugador en el punto de respawn actual
    public void RespawnPlayer(GameObject player)
    {
        // GameManager.instance.ResetHealth();
        if (currentRespawnPoint != null)
        {


            Debug.Log("Respawneando jugador en: " + currentRespawnPoint.position);
            CharacterController controller = player.GetComponent<CharacterController>();

            if (controller != null)
            {
                controller.enabled = false;  // Desactivar temporalmente el Character Controller
            }

            player.transform.position = currentRespawnPoint.position;

            if (controller != null)
            {
                controller.enabled = true;  // Reactivar el Character Controller
            }

             GameManager.instance.ResetHealth();
             HealthBar.instance.SetHealth(100);  // Resetear la vida del jugador
        }
        else
        {
            Debug.LogWarning("No hay punto de respawn establecido.");
        }
    }
}