using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPads : MonoBehaviour
{
    public float jumpForce = 10f; // Fuerza de salto
    public float smoothTime = 0.2f; // Tiempo para suavizar el salto
    public float fallMultiplier = 2.5f; // Multiplicador de caída
    public float lowJumpMultiplier = 2f; // Multiplicador para saltos cortos

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController characterController = other.GetComponent<CharacterController>();
            if (characterController != null)
            {
                StartCoroutine(ApplyJump(characterController));
            }
        }
    }

    private IEnumerator ApplyJump(CharacterController characterController)
    {
        float verticalSpeed = 0f;
        float targetSpeed = jumpForce;
        float currentSpeed = 0f;

        float elapsedTime = 0f;

        while (elapsedTime < smoothTime)
        {
            elapsedTime += Time.deltaTime;
            currentSpeed = Mathf.Lerp(0, targetSpeed, elapsedTime / smoothTime);
            verticalSpeed = currentSpeed;
            characterController.Move(new Vector3(0, verticalSpeed * Time.deltaTime, 0));

            yield return null;
        }

        // Asegúrate de que el jugador suba al máximo al final
        characterController.Move(new Vector3(0, targetSpeed * Time.deltaTime, 0));

        // Comienza a aplicar la gravedad suavizada
        while (true)
        {
            if (characterController.isGrounded)
            {
                break; // Sale si el jugador está en el suelo
            }

            // Aplica la gravedad
            verticalSpeed += Physics.gravity.y * Time.deltaTime;
            // Suaviza la caída
            if (verticalSpeed < 0)
            {
                verticalSpeed *= fallMultiplier; // Aumenta la velocidad de caída
            }
            else if (verticalSpeed > 0 && !Input.GetButton("Jump"))
            {
                verticalSpeed *= lowJumpMultiplier; // Suaviza el salto si el botón de salto no está presionado
            }

            characterController.Move(new Vector3(0, verticalSpeed * Time.deltaTime, 0));
            yield return null;
        }
    }
}