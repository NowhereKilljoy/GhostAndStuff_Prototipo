using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetMaxHealth(GameManager.instance.playerMaxHealth);
    }
    
    
    
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        slider.value = GameManager.instance.playerCurrentHealth;

        fill.color = gradient.Evaluate(1f);    
    }

    public void SetHealth(int health)
    {
        Debug.Log(health);
        Debug.Log("Vida Cambiada");
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
