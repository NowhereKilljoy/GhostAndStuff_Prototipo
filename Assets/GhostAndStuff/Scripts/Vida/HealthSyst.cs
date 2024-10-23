using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSyst : MonoBehaviour
{
    public static HealthSyst instance;
    // private int currentHealth;
    public float damageInterval = 1f; 
    private bool isTakingDamage = false;

    private void Awake()
    {
        instance = this;

    }

    void Start()
    {
        /*GameManager.instance.playerMaxHealth = maxHealth;
        GameManager.instance.TakeDamagePlayer(0); // Inicializa la vida en el GameManager sin reducirla*/
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && !isTakingDamage)
        {
            // Iniciar el Coroutine para el da�o continuo
            isTakingDamage = true;
            StartCoroutine(DamageOverTime());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // Detener el da�o cuando el jugador sale del rango del enemigo
            isTakingDamage = false;
            StopCoroutine(DamageOverTime());
        }
    }

    // Coroutine que aplica da�o cada cierto intervalo de tiempo
    private IEnumerator DamageOverTime()
    {
        while (isTakingDamage)
        {
            GameManager.instance.TakeDamagePlayer(10);

            // Verificamos si la vida del jugador llega a 0, desde el GameManager
            if (GameManager.instance.playerCurrentHealth <= 0)
            {
                SceneManager.LoadScene(1); // Reinicia la escena o lleva a la pantalla de Game Over
                yield break; // Detiene el Coroutine si el jugador muere
            }

            yield return new WaitForSeconds(damageInterval); // Espera antes de aplicar m�s da�o
        }
    }
}
