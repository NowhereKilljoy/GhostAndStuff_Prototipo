using UnityEngine;




public class CheckPoint : MonoBehaviour
{
    public GameObject onSpawner;
    public GameObject offSpawner;

    private Animator onAnimator;

    private void Start()
    {
        if (onSpawner != null)
        {
            onAnimator = onSpawner.GetComponent<Animator>();
        }
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

            if (onAnimator != null)
            {
                onAnimator.Play("ActivoLoop"); // nombre exacto del estado en el Animator
            }
        }
    }
}
