using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public enum AbsorbType {Bullet,Health,Key}
    
    public int playerMaxHealth = 10;
    public int playerCurrentHealth = 10;

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

        }
    }
    
    public void TakeDamagePlayer(int damage)
    {
        playerCurrentHealth -= damage;
        HealthBar.instance.SetHealth(playerCurrentHealth);

        if (playerCurrentHealth <= 0)
        {
            playerCurrentHealth = 0;
            // Aqu� puedes manejar la l�gica de Game Over o reiniciar el nivel si lo deseas
        }
    }
    
}
