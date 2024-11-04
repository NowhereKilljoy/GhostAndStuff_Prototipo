using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbsorb
{
    void GetHealth();
    void GetHealth(int amount);
    public void UpdateAmmo();
    void ShootAmmo();
    void GetAmmo();
    void GetKey();
    void GetKey(int ID);
}
