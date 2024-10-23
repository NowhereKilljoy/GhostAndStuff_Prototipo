using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public int playerMaxHealth = 100;
    public int playerCurrentHealth =100;

    // Diccionario para almacenar la vida de cada enemigo
    private Dictionary<int, int> enemyHealthDict = new Dictionary<int, int>();

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

    // M�todo para registrar un enemigo en el diccionario
    public void RegisterEnemy(int enemyID, int maxHealth)
    {
        if (!enemyHealthDict.ContainsKey(enemyID))
        {
            enemyHealthDict.Add(enemyID, maxHealth);
        }
    }

    // M�todo para reducir la vida de un enemigo
    public void TakeDamageEnemy(int enemyID, int damage)
    {
        if (enemyHealthDict.ContainsKey(enemyID))
        {
            enemyHealthDict[enemyID] -= damage;

            if (enemyHealthDict[enemyID] <= 0)
            {
                DestroyEnemy(enemyID);
            }
        }
    }

    // M�todo para destruir el enemigo
    public void DestroyEnemy(int enemyID)
    {
        // Encontrar al enemigo usando su ID y destruirlo
        EnemyHealth enemy = FindEnemyByID(enemyID);
        if (enemy != null)
        {
            Destroy(enemy.gameObject);  // Destruir el objeto del enemigo
            enemyHealthDict.Remove(enemyID);  // Eliminar del diccionario
            Debug.Log("Enemy with ID " + enemyID + " destroyed.");
        }
    }

    // M�todo auxiliar para encontrar un enemigo por su ID
    private EnemyHealth FindEnemyByID(int enemyID)
    {
        // Buscar el enemigo por su ID (puedes mejorar la b�squeda dependiendo de c�mo gestiones los enemigos)
        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();
        foreach (EnemyHealth enemy in enemies)
        {
            if (enemy.enemyID == enemyID)
            {
                return enemy;
            }
        }
        return null;
    }
}
