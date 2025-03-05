using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

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

           
        }else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
        }
        //cambiar para que no dependa de detectar un objeto para girar
        
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
                    Instantiate(pfBulletProyectile, spawnBulletPosition.position,Quaternion.LookRotation(aimDir, Vector3.up));
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

        if (StarterAssetsInputs.absorb)
        {
            if (hitTransform != null)
            {
                if (hitTransform.GetComponent<Absorb>() != null)
                {
                    Absorb obj = hitTransform.GetComponent<Absorb>();
                    obj.AnimStart();

                    switch (obj.absorb)
                    {
                        case GameManager.AbsorbType.Bullet:                    
                            absorbMec.GetAmmo();
                            break;
                        case GameManager.AbsorbType.Health:
                            absorbMec.GetHealth();
                            break;
                        case GameManager.AbsorbType.Key:
                            absorbMec.GetKey(obj.keyNumber);
                            break;
                    }
                    
                    Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
                }
            }
            StarterAssetsInputs.absorb = false;
        }
        

    }

}
