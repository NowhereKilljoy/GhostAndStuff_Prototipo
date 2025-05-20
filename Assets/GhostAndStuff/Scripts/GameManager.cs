using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum AbsorbType { Bullet, Health, Key }

    [Header("Health Settings")]
    public int playerMaxHealth = 10;
    public int playerCurrentHealth = 10;

    [Header("Animación y Sonido")]
    public Animator playerAnimator;
    public AudioSource healAudio;
    public AudioSource hurtAudio;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            playerCurrentHealth = playerMaxHealth;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    public void SumHealth(int value)
    {
        if (playerCurrentHealth < playerMaxHealth)
        {
            playerCurrentHealth += value;
            if (playerCurrentHealth > playerMaxHealth)
            {
                playerCurrentHealth = playerMaxHealth;
            }

            HealthBar.instance.SetHealth(playerCurrentHealth);

            // 🔊 Sonido de curación
            if (healAudio != null)
            {
                healAudio.Play();
            }

            // 🕹️ Animación de curación
            if (playerAnimator != null)
            {
                playerAnimator.SetBool("Heal", true);
                StartCoroutine(ResetAnimBool("Heal"));
            }
        }
    }

    public void TakeDamagePlayer(int damage)
    {
        playerCurrentHealth -= damage;
        HealthBar.instance.SetHealth(playerCurrentHealth);

        // 🔊 Sonido de daño
        if (hurtAudio != null)
        {
            hurtAudio.Play();
        }

        // 🕹️ Animación de daño
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("Hurt", true);
            StartCoroutine(ResetAnimBool("Hurt"));
        }

        if (playerCurrentHealth <= 0)
        {
            playerCurrentHealth = 0;
            // Aquí se puede  manejar lógica de Game Over, respawn, etc.
        }
    }

    // 🔁 Resetear bool del Animator
    private IEnumerator ResetAnimBool(string boolName)
    {
        yield return new WaitForSeconds(0.1f);
        playerAnimator.SetBool(boolName, false);
    }
}
