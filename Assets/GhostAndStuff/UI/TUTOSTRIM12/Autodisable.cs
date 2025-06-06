using UnityEngine;

public class AutoDisable : MonoBehaviour
{
    public float delay = 10f; // Tiempo en segundos antes de desactivarse

    void OnEnable()
    {
        Invoke(nameof(DisableObject), delay);
    }

    void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
