using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalHealthController : MonoBehaviour
{
    [SerializeField] private DecalProjector decalProjector;
    [SerializeField] public Material[] healthMaterials = new Material[10]; // Array for 10 health states

    private void Start()
    {
        if (decalProjector == null)
            decalProjector = GetComponent<DecalProjector>();
    }

    private void Update()
    {
        UpdateDecalMaterial();
    }

    private void UpdateDecalMaterial()
    {
        float currentHealth = GameManager.instance.playerCurrentHealth;
        int materialIndex = Mathf.Clamp(Mathf.FloorToInt(currentHealth - 1), 0, 9);

        if (healthMaterials != null && healthMaterials.Length > materialIndex)
        {
            decalProjector.material = healthMaterials[materialIndex];
        }
    }
}

