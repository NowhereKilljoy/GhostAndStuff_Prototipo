using System.Collections;
using UnityEngine;


public class HealthSyst : MonoBehaviour
{
    public static HealthSyst instance;
    // private int currentHealth;
    public float damageInterval = 1.5f;
    private bool isTakingDamage = false;
    private bool isDamaging = false;
    private RespawnManager respawnManager;

    //private GameObject player;

    private void Awake()
    {
        instance = this;
        // player = GameObject.FindGameObjectWithTag ("Player");
    }

    void Start()
    {
        respawnManager = FindObjectOfType<RespawnManager>();
        /*GameManager.instance.playerMaxHealth = maxHealth;
        GameManager.instance.TakeDamagePlayer(0); // Inicializa la vida en el GameManager sin reducirla*/

        /* GameManager.instance.ResetHealth();
         HealthBar.instance.SetHealth(100);*/

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && !isTakingDamage)
        {
            // Iniciar el Coroutine para el da o continuo
            isTakingDamage = true;
            StartCoroutine(DamageOverTime());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // Detener el daño cuando el jugador sale del rango del enemigo
            // StopCoroutine(DamageOverTime());
            isTakingDamage = false;
            StopCoroutine(DamageOverTime());
        }
    }

    // Coroutine que aplica da o cada cierto intervalo de tiempo
    private IEnumerator DamageOverTime()
    {
        while (isTakingDamage)
        {
            GameManager.instance.TakeDamagePlayer(10);

            // Verificamos si la vida del jugador llega a 0, desde el GameManager
            if (GameManager.instance.playerCurrentHealth <= 0)
            {
                DeathPlayer(gameObject);
                isTakingDamage = false;
                yield break; // Detiene el Coroutine si el jugador muere
            }

            yield return new WaitForSeconds(1); // Espera antes de aplicar mas daño
        }
        isTakingDamage = false;
    }

    public void DeathPlayer(GameObject player)
    {
        respawnManager.RespawnPlayer(player);
    }
}