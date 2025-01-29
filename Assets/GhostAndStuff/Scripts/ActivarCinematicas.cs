using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarCinematicas : MonoBehaviour
{
 
      
    // Referencia a los objetos que quieres activar
    public GameObject[] objetosParaActivar;

    // Tiempo en segundos para que los objetos se desactiven después de ser activados
    public float tiempoDesactivacion = 3f; // Puedes ajustar este valor desde el Inspector

    // Flag para asegurarse de que el script solo se ejecute una vez
    private bool haActivado = false;

    // Este método se llama cuando otro collider entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra tiene el tag "Player" y si aún no se ha activado
        if (other.CompareTag("Player") && !haActivado)
        {
            // Recorre el array de objetos y activa cada uno
            foreach (GameObject obj in objetosParaActivar)
            {
                obj.SetActive(true);
            }

            // Establece que el script ya ha ejecutado su acción
            haActivado = true;

            // Llama a la corutina para desactivar los objetos después de 'tiempoDesactivacion' segundos
            StartCoroutine(DesactivarObjetos());
        }
    }

    // Corutina que desactiva los objetos después de un tiempo
    private IEnumerator DesactivarObjetos()
    {
        // Espera el tiempo especificado
        yield return new WaitForSeconds(tiempoDesactivacion);

        // Recorre el array de objetos y los desactiva
        foreach (GameObject obj in objetosParaActivar)
        {
            obj.SetActive(false);
        }
    }
}