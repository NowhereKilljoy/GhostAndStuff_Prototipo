using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject activadorVisual;



    private void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            RespawnManager respawnManager = FindObjectOfType<RespawnManager>();
            respawnManager.UpdateRespawnPoint(this.transform);
            Debug.Log("Checkpoint alcanzado en: " + transform.name);
        }
    }
}