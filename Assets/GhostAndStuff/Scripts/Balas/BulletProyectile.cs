using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProyectile : MonoBehaviour
{
    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;
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
        if (other.GetComponent<BulletTarget>() != null)
        {
            // Hit Target, golpear al objetivo
            Instantiate(vfxHitGreen,transform.position, Quaternion.identity);
        } else
        {
            //Hit Something else, golpeo algo más
            Instantiate(vfxHitRed, transform.position, Quaternion.identity);
        }            
        Destroy(gameObject);
    }









}
