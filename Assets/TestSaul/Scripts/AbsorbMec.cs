using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class AbsorbMec : MonoBehaviour,IAbsorb
{
    [Header("Keys")]
    [SerializeField] bool Key1 = false;
    [SerializeField] bool Key2 = false;
    [SerializeField] bool Key3 = false;
    
    [Header("Cresta Ammo")]
    [SerializeField] Material AmmoMat;
    [SerializeField] Material NoAmmoMat;
    
    
    [SerializeField] GameObject Cresta1;
    [SerializeField] GameObject Cresta2;
    [SerializeField] GameObject Cresta3;
    
    private StarterAssetsInputs StarterAssetsInputs;

    private void Start()
    {
        StarterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if (StarterAssetsInputs.absorb)
        {
            
        }
    }

    public void InitAbsorb()
    {
        throw new System.NotImplementedException();
    }

    public void GetHealth()
    {
        throw new System.NotImplementedException();
    }

    public void GetAmmo()
    {
        
    }

    public void GetKey()
    {
        throw new System.NotImplementedException();
    }
}
