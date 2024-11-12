using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class EnemyManager : MonoBehaviour, IObserver
{
    
    private Dictionary<int, int> enemyHealthDict = new Dictionary<int, int>();

    //public void RegisterEnemy(int enemyID, int maxHealth)

    private void Start()
    {
        FindAllEnemy();
    }

    int index;
    public void RegisterEnemy(EnemyHealth _enemy)
    {
        _enemy.SetID(index);
        if (!enemyHealthDict.ContainsKey(_enemy.enemyID))
        {
            enemyHealthDict.Add(_enemy.enemyID, _enemy.maxHealth);
            Debug.Log("Enemigo Registrado" + _enemy.enemyID);
        }
    }

    //public void TakeDamageEnemy(int enemyID, int damage)
    public void TakeDamageEnemy(EnemyHealth _enemy)
    {
        Debug.Log("Enemigo damaged: " + _enemy.enemyID);
        if (enemyHealthDict.ContainsKey(_enemy.enemyID))
        {
            enemyHealthDict[_enemy.enemyID] -= _enemy.damageE;

            if (enemyHealthDict[_enemy.enemyID] <= 0)
            {
                //DestroyEnemy(_enemy.enemyID);
                EnemyDeath(_enemy);
            }
        }
    }

    public void EnemyDeath(EnemyHealth _enemy)
    {
        Debug.Log("Enemigo desactivate: " + _enemy.enemyID);
        enemyHealthDict.Remove(_enemy.enemyID);
        _enemy.gameObject.SetActive(false);
    }

    public void DestroyEnemy(int enemyID)
    {
        // Encontrar al enemigo usando su ID y destruirlo
        EnemyHealth enemy = FindEnemyByID(enemyID);
        if (enemy != null)
        {
            Object.Destroy(enemy.gameObject);  // Destruir el objeto del enemigo
            enemyHealthDict.Remove(enemyID);  // Eliminar del diccionario
            Debug.Log("Enemy with ID " + enemyID + " destroyed.");
        }
    }

    private EnemyHealth FindEnemyByID(int enemyID)
    {
        // Buscar el enemigo por su ID (puedes mejorar la b�squeda dependiendo de c�mo gestiones los enemigos)
        EnemyHealth[] enemies = Object.FindObjectsOfType<EnemyHealth>();
        foreach (EnemyHealth enemy in enemies)
        {
            if (enemy.enemyID == enemyID)
            {
                return enemy;
            }
        }
        return null;
    }
    private void FindAllEnemy()
    {
        EnemyHealth[] enemies = Object.FindObjectsOfType<EnemyHealth>();
        foreach (EnemyHealth enemy in enemies)
        {
            
            enemy.SuscribeNotification(this);
            
            enemy.Notify(0);
            index++;
        }
        
    }

    public void ClearDict()
    {
        enemyHealthDict.Clear();
    }

    public void Updated(INotifications notify, int idEvent)
    {
        if (notify is EnemyHealth)
        {
            EnemyHealth enemy = (EnemyHealth)notify;
            switch (idEvent)
            {
                case 0:
                    RegisterEnemy(enemy);
                    break;
                case 1:
                    TakeDamageEnemy(enemy);
                    break;
                case 2:
                    EnemyDeath(enemy);
                    break;
                default:
                    Debug.LogWarning("idEvent desconocida: " + idEvent);
                    break;
            }
        }

        
    }
}