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
    public float verticalAngle = 15f; // Angle for vertical shooting
    public float speed = 20f; // Bullet speed

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
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
            return;

        // Calculate turret rotation
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        // Check if the player is within the turret's field of view
        if (Physics.Raycast(transform.position, dir, range))
        {
            // Fire bullet
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
        // Get the direction the turret is facing
        Vector3 direction = firePoint.forward;

        // Randomly choose a vertical angle
        float angle = Random.Range(-verticalAngle, verticalAngle);
        Quaternion bulletRotation = Quaternion.Euler(angle, 0f, 0f);

        // Instantiate bullet with adjusted rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * bulletRotation);

        // Set the bullet's initial velocity
        bullet.GetComponent<Rigidbody>().velocity = direction * speed;
    }


}
