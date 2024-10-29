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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryRaycast();
        }
    }


    // Método para recibir daño
    public void TryRaycast()
    {
        RaycastHit hit;

        // Crear un rayo desde la cámara hacia la posición del mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit))
        {

            if (hit.collider.gameObject == gameObject)
            {

                GameManager.instance.TakeDamageEnemy(enemyID, damageE);
                Debug.Log("Enemigo golpeado: " + enemyID + "  Aplicando daño: " + damageE);
            }
        }
    }
}