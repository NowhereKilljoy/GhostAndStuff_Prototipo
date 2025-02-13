using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System;

public class DisparoRaycastGNS : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProyectile;
    [SerializeField] private Transform spawnBulletPosition;

    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;

    private ThirdPersonController thirdPersonController;
    private AbsorbMec absorbMec;
    private StarterAssetsInputs StarterAssetsInputs;

    private readonly List<IObserver> _observers = new List<IObserver>();

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        StarterAssetsInputs = GetComponent<StarterAssetsInputs>();
        absorbMec = GetComponent<AbsorbMec>();

    }
    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        Transform hitTransform = null;
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
            hitTransform = raycastHit.transform;



            Debug.DrawRay(ray.origin, ray.direction.normalized * debugTransform.position.magnitude, Color.red);
        }
        if (StarterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);


        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
        }
        Vector3 worldAimTarget = mouseWorldPosition;
        worldAimTarget.y = transform.position.y;
        Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        if (StarterAssetsInputs.shoot)
        {
            if (hitTransform != null)
            {
                if (hitTransform.GetComponent<BulletTarget>() != null)
                {
                    // Hit Something , golpeo algo
                    Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
                }
                else
                {
                    //Hit Something else, golpeo algo m�s
                    Instantiate(vfxHitRed, transform.position, Quaternion.identity);
                }

                if (absorbMec.Amount > 0)
                {
                    Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
                    Instantiate(pfBulletProyectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
                    absorbMec.ShootAmmo();
                    if (hitTransform.GetComponent<INotifications>() != null)
                    {
                        Debug.Log("a");
                        hitTransform.GetComponent<INotifications>().Notify(1);
                    }

                }

                StarterAssetsInputs.shoot = false;
            }
        }

        // Si estamos absorbiendo y el estado cambia
        if (StarterAssetsInputs.absorb)
        {
            // Si no estamos absorbiendo, iniciar absorción
            if (!isAbsorbing && hitTransform != null)
            {
                AbsorbDetected(hitTransform);
            }
            // Si estamos absorbiendo pero el hitTransform es nulo, cancelar
            else if (isAbsorbing && hitTransform == null)
            {
                CancelAbsorb();
            }
        }
        // Si no estamos absorbiendo y estábamos absorbiendo, cancelar
        else if (isAbsorbing)
        {
            CancelAbsorb();
        }
    }


    private bool isAbsorbing = false;
    ForAbsorb objAbs;
    private void AbsorbDetected(Transform hitTransform)
    {
        if (hitTransform.GetComponent<ForAbsorb>() != null)
        {
            isAbsorbing = true;
            objAbs = hitTransform.GetComponent<ForAbsorb>();
            objAbs.AnimStart();
            StartCoroutine(Reset(objAbs.timeAbsorb));
        
        }
    }


    private void CancelAbsorb()
    {
        isAbsorbing = false;
        objAbs.AnimInterrupt();
        StopCoroutine(Reset(objAbs.timeAbsorb));
        Debug.LogWarning("Ya no Absorbe");
    }

    public IEnumerator Reset(float time)
    {
        yield return new WaitForSeconds(time);
        switch (objAbs.absorb)
        {
            case GameManager.AbsorbType.Bullet:
                absorbMec.GetAmmo();
                break;
            case GameManager.AbsorbType.Health:
                absorbMec.GetHealth();
                break;
            case GameManager.AbsorbType.Key:
                absorbMec.GetKey(objAbs.keyNumber);
                break;
        }
        isAbsorbing = false;
    }


}
