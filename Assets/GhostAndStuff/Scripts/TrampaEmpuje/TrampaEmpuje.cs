using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaEmpuje : MonoBehaviour
{
    
    public float pushForce = 10f; // Fuerza de empuje
    public float smoothTime = 0.2f; // Tiempo para suavizar el empuje, de nuevo, si no se siente antinatural pero para mal

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
        Vector3 pushDirection = -playerTransform.forward; // el transform en negativo sirve para que la fuerza se aplique en la dirección opuesta a la orientación del objeto
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

        
        characterController.Move(pushDirection * pushForce * Time.deltaTime);
    }
}//el tiempo es una ilusion, todo es mentira, compra oro!!!
