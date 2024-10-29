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


    // M�todo para recibir da�o
    public void TryRaycast()
    {
        RaycastHit hit;

        // Crear un rayo desde la c�mara hacia la posici�n del mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit))
        {

            if (hit.collider.gameObject == gameObject)
            {

                GameManager.instance.TakeDamageEnemy(enemyID, damageE);
                Debug.Log("Enemigo golpeado: " + enemyID + "  Aplicando da�o: " + damageE);
            }
        }
    }
}