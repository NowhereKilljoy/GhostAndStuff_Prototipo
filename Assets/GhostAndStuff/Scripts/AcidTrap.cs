using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AcidTrap : MonoBehaviour
{
    public int damagePerSecond = 10; // Cantidad de daño por segundo
    private bool isPlayerInTrap = false; // Verifica si el jugador está en la trampa

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrap = true;
            StartCoroutine(DamagePlayerOverTime());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrap = false;
        }
    }

    private IEnumerator DamagePlayerOverTime()
    {
        while (isPlayerInTrap)
        {
            GameManager.instance.TakeDamagePlayer(damagePerSecond); // Llamada al GameManager para reducir vida
            if (GameManager.instance.playerCurrentHealth <= 0)
            {
                SceneManager.LoadScene(0);
            }
            yield return new WaitForSeconds(1); // Aplica daño cada segundo
        }
    }
}
