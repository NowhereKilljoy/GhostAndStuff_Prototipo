using UnityEngine;

public class Llave : MonoBehaviour
{

    public GameObject puerta; // La puerta a la que la llave da acceso
    public Transform player;
    public bool takeLlave = false;

    // Variables para el movimiento oscilante
    public float oscilacionAmplitud = 0.5f; // Qué tan alto y bajo se moverá la llave
    public float oscilacionVelocidad = 1.0f; // Qué tan rápido se moverá la llave hacia arriba y abajo

   // public AudioSource audiS;
   // public AudioClip getClip;

    private Vector3 posicionInicial; // La posición original de la llave antes de comenzar a oscilar

    private void Start()
    {
        posicionInicial = transform.position;
    }

    private void Update()
    {
        // Si la llave no ha sido tomada, realizar movimiento oscilante
        if (!takeLlave)
        {
            Oscilar();
        }
    }

    private void Oscilar()
    {
        transform.position = posicionInicial + new Vector3(0, Mathf.Sin(Time.time * oscilacionVelocidad) * oscilacionAmplitud, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //audiS.PlayOneShot(getClip);
            player = other.transform;
            takeLlave = true;
            Invoke("TakeKey", 0.6f);
        }
    }

    private void TakeKey()
    {
        if (takeLlave)
        {
            puerta.GetComponent<Puerta>().isUnlocked = true;
            Destroy(gameObject);
        }
    }
}
