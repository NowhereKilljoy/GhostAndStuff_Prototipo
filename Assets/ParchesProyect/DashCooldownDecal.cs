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

        if (decalProjector != null)
        {
            decalProjector.material = instancedMaterial;
            // Set initial texture to ready state
            instancedMaterial.SetTexture(BaseMapProperty, readyTexture);
        }
        else
        {
            Debug.LogError("Decal Projector is missing! Please assign it in the inspector.");
        }
    }

    private void Update()
    {
        if (ThirdPersonController.Instance != null)
        {
            // Check if the player is currently on dash cooldown
            isOnCooldown = (Time.time < playerController.nextDashTime);

            //if (ThirdPersonController.Instance.ResetCanDash )
            //{
            //    instancedMaterial.SetTexture(BaseMapProperty, readyTexture);

            //}
            //else
            //{
            //    // Player is ready to dash

            //    instancedMaterial.SetTexture(BaseMapProperty, coolingDownTexture);

            //}
        }
    }

    // Helper method to access the nextDashTime from the ThirdPersonController
    private float GetNextDashTime()
    {
        // Use reflection to access the private nextDashTime field from ThirdPersonController
        var field = playerController.GetType().GetField("nextDashTime",
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance);

        if (field != null)
        {
            return (float)field.GetValue(playerController);
        }

        // Fallback method if reflection fails
        // Calculate based on the public isDashing property and dashCooldown value
        if (playerController.isDashing)
        {
            return Time.time + playerController.dashCooldown;
        }

        return 0f; // If we can't determine, assume ready
    }

    // Optional: Helper method to directly get the dash status
    public bool IsDashReady()
    {
        if (playerController != null)
        {
            return Time.time < playerController.nextDashTime;
        }
        return false;
    }
}