using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class ThirdPersonShooterControlerGNS : MonoBehaviour
{
    [SerializeField]  private CinemachineVirtualCamera aimVirtualCamera;

    private StarterAssetsInputs StarterAssetsInputs;

    private void Awake()
    {
        StarterAssetsInputs = GetComponent <StarterAssetsInputs>();
    }
    private void Update()
    {
        if (StarterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
        }
    }

}
