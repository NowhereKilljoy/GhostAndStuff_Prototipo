using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DashCooldownDecal : MonoBehaviour
{
    
    [Header("Decal Settings")]
    [SerializeField] private DecalProjector decalProjector;
    [SerializeField] private Material decalMaterial;

    [Header("Dash Cooldown Settings")]
    [SerializeField] private Texture2D readyTexture;
    [SerializeField] private Texture2D coolingDownTexture;

    [Header("Player Reference")]
    [SerializeField] private ThirdPersonController playerController;

    private float cooldownTimer = 0f;
    public bool isOnCooldown = true;
    private Material instancedMaterial;


    private static readonly int BaseMapProperty = Shader.PropertyToID("_BaseMap");

    
    private void Start()
    {
        
        // Create an instance of the material to avoid modifying the original asset
        instancedMaterial = new Material(decalMaterial);
        decalProjector.material = instancedMaterial;
        // Assign the instanced material to the decal projector
        if (decalProjector == null)
        {
            decalProjector = GetComponent<DecalProjector>();
        }

        if (playerController == null)
        {
            
            if (playerController == null)
            {
                              
                Debug.LogError("ThirdPersonController not found! Please assign it in the inspector.");
            }
        }

        if (ThirdPersonController.Instance.canDash)
        {
            // Set initial texture to ready state
            instancedMaterial.SetTexture(BaseMapProperty, readyTexture);
        }
        else
        {
            instancedMaterial.SetTexture(BaseMapProperty, coolingDownTexture);
        }
    }

    private void Update()
    {
        if (ThirdPersonController.Instance != null)
        {
            // Check if the player is currently on dash cooldown
            isOnCooldown = (Time.time < playerController.nextDashTime);

            if (ThirdPersonController.Instance.canDash)
            {
                instancedMaterial.SetTexture(BaseMapProperty, readyTexture);

            }
            else
            {
                // Player is ready to dash

                instancedMaterial.SetTexture(BaseMapProperty, coolingDownTexture);

            }
        }
    }

}