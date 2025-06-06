using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public Transform[] respawnPoints;
    public GameObject[] respawnIndicators;

    private Transform currentRespawnPoint;
    private CheckPoint lastActivatedCheckpoint;

    public static RespawnManager instance;

    void Start()
    {
        if (respawnPoints.Length > 0)
        {
            currentRespawnPoint = respawnPoints[0];
        }
    }

    public void UpdateRespawnPoint(Transform newRespawnPoint)
    {
        currentRespawnPoint = newRespawnPoint;
        Debug.Log("Punto de respawn actualizado a: " + newRespawnPoint.name);

        // Desactivar animación en el checkpoint anterior
        if (lastActivatedCheckpoint != null)
        {
            lastActivatedCheckpoint.SetCheckpointActive(false);
        }

        // Activar visualmente este nuevo checkpoint
        CheckPoint nuevoCheckpoint = newRespawnPoint.GetComponent<CheckPoint>();
        if (nuevoCheckpoint != null)
        {
            nuevoCheckpoint.SetCheckpointActive(true);
            lastActivatedCheckpoint = nuevoCheckpoint;
        }
    }

    public void RespawnPlayer(GameObject player)
    {
        if (currentRespawnPoint != null)
        {
            Debug.Log("Respawneando jugador en: " + currentRespawnPoint.position);
            CharacterController controller = player.GetComponent<CharacterController>();

            if (controller != null)
            {
                controller.enabled = false;
            }

            player.transform.position = currentRespawnPoint.position;

            if (controller != null)
            {
                controller.enabled = true;
            }

            GameManager.instance.ResetHealth();
            HealthBar.instance.SetHealth(100);
        }
        else
        {
            Debug.LogWarning("No hay punto de respawn establecido.");
        }
    }
}
