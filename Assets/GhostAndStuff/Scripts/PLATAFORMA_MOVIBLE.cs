using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLATAFORMA_MOVIBLE : MonoBehaviour
{
    public GameObject Player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Player.transform.parent = null;
    }
}
