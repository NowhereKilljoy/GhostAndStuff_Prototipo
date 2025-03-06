using UnityEngine;


public class CheckPoint : MonoBehaviour
{
    public GameObject onSpawner;
    public GameObject offSpawner;



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

            onSpawner.SetActive(true);
            offSpawner.SetActive(false);
        }
    }
}