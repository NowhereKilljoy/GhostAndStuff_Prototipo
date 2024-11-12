using UnityEngine;

public class ResapawnCollider : MonoBehaviour
{
    private RespawnManager respawnManager;

    void Start()
    {
        respawnManager = FindObjectOfType<RespawnManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador ha ca�do. Respawneando...");
            respawnManager.RespawnPlayer(other.gameObject);
        }
    }
}
