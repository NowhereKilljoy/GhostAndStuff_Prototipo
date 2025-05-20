using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHealth : MonoBehaviour, INotifications
{
    public int maxHealth;
    public int enemyID;
    public int damageE;

    [Header("Prefab al Recibir Daño")]
    public GameObject hitEffectPrefab;   // Prefab que aparece al recibir daño
    public AudioClip hitSound;           // Nuevo sonido al recibir daño

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

        if (idEvent == 1) // Evento de recibir daño
        {
            PlayHitEffect();
            PlayHitSound(); //  Reproducir sonido de daño
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

    private void PlayHitSound()
    {
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    public void PlayDeathEffects()
    {
        // Detener movimiento al morir
        ComportamientoEnemigoBASE comportamiento = GetComponent<ComportamientoEnemigoBASE>();
        if (comportamiento != null)
        {
            comportamiento.isDead = true;
        }

        // Animación de muerte
        if (animator != null)
        {
            animator.SetTrigger("death");
        }

        // Sonido de muerte
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        // Prefab visual de muerte
        if (deathPrefab != null)
        {
            GameObject obj = Instantiate(deathPrefab, transform.position, transform.rotation);
            Destroy(obj, destroyDelay + 2f);
        }

        // Desactivarse luego de un tiempo
        StartCoroutine(DesactivarDespuesDeTiempo(destroyDelay));
    }

    private IEnumerator DesactivarDespuesDeTiempo(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        gameObject.SetActive(false);
    }
}
