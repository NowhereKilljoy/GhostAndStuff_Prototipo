using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Serialization;

public class AbsorbMec : MonoBehaviour, IAbsorb
{
    [Header("Keys")]
    [SerializeField] bool Key1 = false;
    [SerializeField] bool Key2 = false;
    [SerializeField] bool Key3 = false;

    [Header("Cresta Ammo")] [SerializeField]
    Material AmmoMat;

    [SerializeField] Material NoAmmoMat;

    [SerializeField] GameObject[] crestas;

    [Header("AmmoInfo")] 
    [SerializeField] private int ammoAmount = 0; public int Amount { get { return ammoAmount; } }
    [SerializeField] private int maxAmmo = 3; public int MaxA { get { return maxAmmo; } }


    private StarterAssetsInputs StarterAssetsInputs;

    private void Start()
    {
        StarterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    public void GetHealth()
    {
        GameManager.instance.SumHealth(10);
    }

    public void GetHealth(int amount)
    {
        GameManager.instance.SumHealth(amount);
    }

    public void UpdateAmmo()
    {
        for (int i = 0; i < maxAmmo; i++)
        {
            if (crestas[i].GetComponent<Material>() != AmmoMat && ammoAmount >= i + 1)
            {
                crestas[i].GetComponent<MeshRenderer>().material = AmmoMat;
            }
            else
            {
                crestas[i].GetComponent<MeshRenderer>().material = NoAmmoMat;
            }
        }
    }

    public void ShootAmmo()
    {
        ammoAmount--;
        UpdateAmmo();
    }

    public void GetAmmo()
    {
        if (ammoAmount >= 3)
        {
            Debug.Log("Municion Llena");
        }
        else
        {
            //ammoAmount += 1;
            ammoAmount = 3;
        }
        UpdateAmmo();
    }

    public void GetKey()
    {
        Debug.Log("Not Implemented");
    }

    public void GetKey(int ID)
    {
        if (ID < 3)
        {
            {
                switch (ID)
                {
                    case 0:
                        Key1 = true;
                        break;
                    case 1:
                        Key2 = true;
                        break;
                    case 2:
                        Key3 = true;
                        break;
                    default:
                        Debug.Log("ID Error");
                        break;
                }
            }
        }
    }

    
    
}
