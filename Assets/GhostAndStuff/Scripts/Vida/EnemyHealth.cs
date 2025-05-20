using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHealth : MonoBehaviour, INotifications
{
    public int maxHealth;
    public int enemyID;
    public int damageE;

    [Header("Prefab al Recibir Da�o")]
    public GameObject hitEffectPrefab;   // Prefab que aparece al recibir da�o

    [Header("Muerte")]
    public GameObject deathPrefab;       // Prefab que aparece al morir
    public float destroyDelay = 3f;
    public AudioClip deathSound;
    private AudioSource audioSource;
    private Animator animator;

    private readonly List<IObserver> _observers = new List<IObserver>();

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void SetID(int newid)
    {
        enemyID = newid;
    }

    public void SuscribeNotification(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void UnSuscribeNotification(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify(int idEvent)
    {
        foreach (var observer in _observers)
        {
            observer.Updated(this, idEvent);
        }

        // Instanciar efecto visual al recibir da�o
        if (idEvent == 1) // Evento de recibir da�o
        {
            PlayHitEffect();
        }
    }

    private void PlayHitEffect()
    {
        if (hitEffectPrefab != null)
        {
            GameObject obj = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            Destroy(obj, 2f);
        }
    }

    public void PlayDeathEffects()
    {
        // 1. Animaci�n
        if (animator != null)
        {
            animator.SetTrigger("death"); // Trigger del Animator
        }

        // 2. Sonido
        if (deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        // 3. Prefab visual
        if (deathPrefab != null)
        {
            GameObject obj = Instantiate(deathPrefab, transform.position, transform.rotation);
            Destroy(obj, destroyDelay + 2f);
        }

        // 4. Desactivarse despu�s del delay
        StartCoroutine(DesactivarDespuesDeTiempo(destroyDelay));
    }

    private IEnumerator DesactivarDespuesDeTiempo(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        gameObject.SetActive(false);
    }
}
