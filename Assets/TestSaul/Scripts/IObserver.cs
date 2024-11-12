using UnityEngine;

public interface IObserver
{
    //void Updated(INotifications notify);
    [Tooltip("0 for Register, 1 for Damage, 2 for NoHealth")]
    void Updated(INotifications notify, int idEvent); // ID de Eventos Enemigos: 0 Registrar ENEMY DESACT, 1 Hacer Daño Enemy EXCLUSIVO PLAYER, 2 Vida Baja ENEMY DESACT
}
