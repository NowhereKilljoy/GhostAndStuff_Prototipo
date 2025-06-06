using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject onSpawner;
    public GameObject offSpawner;

    [Header("Sonido de activación en loop")]
    public AudioClip activationLoopSound;
    private AudioSource audioSource;


    private void Start()
    {
        // Asegurar que tenga un AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.loop = true; // Habilitar loop
        audioSource.playOnAwake = false;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Actualizar el respawn en el RespawnManager
            RespawnManager respawnManager = FindObjectOfType<RespawnManager>();
            respawnManager.UpdateRespawnPoint(this.transform);

            Debug.Log("Checkpoint alcanzado en: " + transform.name);

            // Activar animación visual
            onSpawner.SetActive(true);
            offSpawner.SetActive(false);

            // Reproducir sonido en loop solo si no está ya sonando
            if (activationLoopSound != null && !audioSource.isPlaying)
            {
                audioSource.clip = activationLoopSound;
                audioSource.Play();
            }

            // Detener el sonido de los demás checkpoints
            CheckPoint[] allCheckpoints = FindObjectsOfType<CheckPoint>();
            foreach (CheckPoint cp in allCheckpoints)
            {
                if (cp != this && cp.audioSource != null)
                {
                    cp.audioSource.Stop();
                }
            }
        }
    }

    public void SetCheckpointActive(bool isActive)
    {
        if (onSpawner != null)
            onSpawner.SetActive(isActive);

        if (offSpawner != null)
            offSpawner.SetActive(!isActive);

        if (audioSource != null)
        {
            if (isActive)
            {
                if (!audioSource.isPlaying && activationLoopSound != null)
                {
                    audioSource.clip = activationLoopSound;
                    audioSource.loop = true;
                    audioSource.Play();
                }
            }
            else
            {
                audioSource.Stop();
            }
        }
    }
}
