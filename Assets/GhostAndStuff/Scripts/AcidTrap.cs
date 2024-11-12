using System.Collections;
using UnityEngine;

public class AcidTrap : MonoBehaviour
{
    public int damagePerSecond = 10; // Cantidad de da o por segundo
    private bool isPlayerInTrap = false; // Verifica si el jugador est  en la trampa
    private GameObject player;
    private bool isDamaging = false;
    private RespawnManager respawnManager;

    private void Start()
    {
        // player = FindObjectWithTag("Player");

        respawnManager = FindObjectOfType<RespawnManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isDamaging)
        {
            player = other.gameObject;
            isPlayerInTrap = true;
            isDamaging = true;
            StartCoroutine(DamagePlayerOverTime());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrap = false;
            isDamaging = false;
        }
    }

    private IEnumerator DamagePlayerOverTime()
    {
        while (isPlayerInTrap)
        {
            GameManager.instance.TakeDamagePlayer(damagePerSecond); // Llamada al GameManager para reducir vida
            if (GameManager.instance.playerCurrentHealth <= 0)
            {
                //SceneManager.LoadScene(1);
                isDamaging = false;
                respawnManager.RespawnPlayer(player);
                //  HealthSyst.instance.DeathPlayer(player);
                yield break;
            }
            yield return new WaitForSeconds(1); // Aplica da o cada segundo
        }
        isDamaging = false;
    }
}
