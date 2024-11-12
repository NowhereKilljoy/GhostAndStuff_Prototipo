using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHealth : MonoBehaviour, INotifications
{
    public int maxHealth;
    public int enemyID;
    public int damageE;
    private readonly List<IObserver> _observers = new List<IObserver>();

    void Start()
    {
        //EnemyManager.RegisterEnemy(enemyID, maxHealth);
    }

    public void SetID(int newid)
    {
        enemyID = newid;
    }
    
   public void SuscribeNotification(IObserver observer)
    {
        _observers.Add(observer);
        
    }

    public void UnSuscribeNotification(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify(int idEvent)
    {
        foreach (var observer in _observers)
        {
            observer.Updated(this , idEvent);
        }
    }

    // Método para recibir daño (En desuso)
    public void TryRaycast()
    {
        RaycastHit hit;

        // Crear un rayo desde la cámara hacia la posición del mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit))
        {

            if (hit.collider.gameObject == gameObject)
            {
                //EnemyManager.TakeDamageEnemy(enemyID, damageE);
                Debug.Log("Enemigo golpeado: " + enemyID + "  Aplicando daño: " + damageE);
            }
        }
    }
    
}