using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    public int enemyID;
    public int damageE;

    void Start()
    {

        GameManager.instance.RegisterEnemy(enemyID, maxHealth);
    }

    // Método para recibir daño
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("BalaFantasmal"))
        {
            // Reducir la vida del enemigo usando el GameManager
            GameManager.instance.TakeDamageEnemy(enemyID, damageE);


            Destroy(other.gameObject);
        }
    }
}
