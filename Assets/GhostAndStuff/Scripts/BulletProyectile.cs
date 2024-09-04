using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProyectile : MonoBehaviour
{
    private Rigidbody bulletRigidBody; 
    private void Awake()
    {
        bulletRigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 42f;
        bulletRigidBody.velocity = transform.forward * speed;
    }


    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }









}
