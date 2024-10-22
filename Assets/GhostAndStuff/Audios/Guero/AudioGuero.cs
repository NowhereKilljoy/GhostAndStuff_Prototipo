using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGuero : MonoBehaviour
{
    public AudioSource audioSource; // Arrastra aquí tu AudioSource en el inspector

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 1 es el botón derecho del ratón
        {
            PlaySound();
        }
    }

    void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}