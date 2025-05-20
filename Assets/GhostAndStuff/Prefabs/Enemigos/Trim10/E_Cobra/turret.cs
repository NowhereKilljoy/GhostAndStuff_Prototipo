using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{
    public Transform target;
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float verticalAngle = 15f;
    public float speed = 20f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip idleSound;
    public AudioClip shootSound;
    private AudioClip currentClip;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        PlayIdleSound();
    }

    void UpdateTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerBody");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestPlayer = null;

        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < shortestDistance)
            {
                shortestDistance = distanceToPlayer;
                nearestPlayer = player;
            }
        }

        if (nearestPlayer != null && shortestDistance <= range)
        {
            target = nearestPlayer.transform;
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null)
        {
            if (animator != null)
                animator.SetTrigger("Idle");

            PlayIdleSound();
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (Physics.Raycast(transform.position, dir, range))
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        if (animator != null)
            animator.SetTrigger("Shoot");

        Vector3 direction = firePoint.forward;
        float angle = Random.Range(-verticalAngle, verticalAngle);
        Quaternion bulletRotation = Quaternion.Euler(angle, 0f, 0f);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * bulletRotation);
        bullet.GetComponent<Rigidbody>().velocity = direction * speed;

        PlayShootSound();
    }

    void PlayIdleSound()
    {
        if (audioSource != null && idleSound != null && currentClip != idleSound)
        {
            audioSource.clip = idleSound;
            audioSource.loop = true;
            audioSource.Play();
            currentClip = idleSound;
        }
    }

    void PlayShootSound()
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
