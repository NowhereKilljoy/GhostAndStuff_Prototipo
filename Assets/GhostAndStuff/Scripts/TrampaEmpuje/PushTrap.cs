using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTrap : MonoBehaviour
{
    
    public float pushForce = 10f; // Fuerza de empuje
    public float smoothTime = 0.2f; // Tiempo para suavizar el empuje

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController characterController = other.GetComponent<CharacterController>();
            if (characterController != null)
            {
                StartCoroutine(ApplyPush(characterController, other.transform));
            }
        }
    }

    private IEnumerator ApplyPush(CharacterController characterController, Transform playerTransform)
    {
        Vector3 pushDirection = -playerTransform.forward; // Direcci�n opuesta a la orientaci�n del objeto
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

        // Aseg�rate de aplicar la fuerza total al final
        characterController.Move(pushDirection * pushForce * Time.deltaTime);
    }
}
