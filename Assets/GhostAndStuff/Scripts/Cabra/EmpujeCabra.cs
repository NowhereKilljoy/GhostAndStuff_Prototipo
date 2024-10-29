using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpujeCabra : MonoBehaviour
{

    public AudioSource audioSource;
    public float pushForce = 10f; // Fuerza de empuje
        public float smoothTime = 0.2f; // Tiempo para suavizar el empuje

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
            audioSource.Play();

            CharacterController characterController = other.GetComponent<CharacterController>();
                if (characterController != null)
                {
                    StartCoroutine(ApplyPush(characterController));
                }
            }
        }
    
    private IEnumerator ApplyPush(CharacterController characterController)
        {
            // Obtener la dirección en X (puedes personalizar la dirección aquí)
            Vector3 pushDirection = transform.right; // Empujar hacia la derecha
            float elapsedTime = 0f;
            float currentForce = 0f;

            // Empuje suave
            while (elapsedTime < smoothTime)
            {
                elapsedTime += Time.deltaTime;
                currentForce = Mathf.Lerp(0, pushForce, elapsedTime / smoothTime);
                characterController.Move(pushDirection * currentForce * Time.deltaTime);

                yield return null;
            }

            // Asegúrate de aplicar la fuerza total al final
            characterController.Move(pushDirection * pushForce * Time.deltaTime);
        }
    }

