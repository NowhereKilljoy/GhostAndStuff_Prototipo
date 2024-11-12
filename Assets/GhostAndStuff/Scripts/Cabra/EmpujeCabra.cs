using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpujeCabra : MonoBehaviour
{
    //Este script va en la cabeza o cuernos del enemigo cabra
    public float pushForce = 10f; // Fuerza de empuje
        public float smoothTime = 0.2f; // Tiempo para suavizar el empuje

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {

            CharacterController characterController = other.GetComponent<CharacterController>();
                if (characterController != null)
                {
                    StartCoroutine(ApplyPush(characterController));
                }
            }
        }
    
    private IEnumerator ApplyPush(CharacterController characterController)
        {
            // Este Script es similar a los jumpads y las trampas de empuje pero solo aplica fuerza en la dirección de X 
            Vector3 pushDirection = transform.forward; 
            float elapsedTime = 0f;
            float currentForce = 0f;

            // Suavizado del empuje 
            while (elapsedTime < smoothTime)
            {
                elapsedTime += Time.deltaTime;
                currentForce = Mathf.Lerp(0, pushForce, elapsedTime / smoothTime);
                characterController.Move(pushDirection * currentForce * Time.deltaTime);

                yield return null;
            }

            
            characterController.Move(pushDirection * pushForce * Time.deltaTime);
        }
    }

