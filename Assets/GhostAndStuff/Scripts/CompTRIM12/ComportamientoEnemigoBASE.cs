using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoEnemigoBASE : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator ani;
    public Quaternion angulo;
    public float grado;

    public GameObject target;
    public bool atacando;
    private float distanciaAtaque = 1.5f;
    private float distanciaMaxima = 10f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip idleClip;
    public AudioClip walkClip;
    public AudioClip runClip;
    public AudioClip attackClip;

    private AudioClip currentClip;

    [Header("Estado de vida")]
    public bool isDead = false;  // se activa al morir

    void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.Find("PlayerArmature");

        PlaySound(idleClip);
    }

    public void Comportamiento_Enemigo()
    {
        if (isDead) return;  //Si está muerto el enemigo ya no hace nada

        float distancia = Vector3.Distance(transform.position, target.transform.position);

        if (distancia > distanciaMaxima)
        {
            ani.SetBool("run", false);
            ani.SetBool("attack", false);
            atacando = false;

            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }

            switch (rutina)
            {
                case 0:
                    ani.SetBool("walk", false);
                    PlaySound(idleClip);
                    break;

                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;

                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    ani.SetBool("walk", true);
                    PlaySound(walkClip);
                    break;
            }
        }
        else if (distancia > distanciaAtaque && !atacando)
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);

            ani.SetBool("walk", false);
            ani.SetBool("run", true);
            ani.SetBool("attack", false);
            transform.Translate(Vector3.forward * 2 * Time.deltaTime);

            PlaySound(runClip);
        }
        else if (distancia <= distanciaAtaque)
        {
            ani.SetBool("walk", false);
            ani.SetBool("run", false);

            if (!atacando)
            {
                atacando = true;
                ani.SetBool("attack", true);
                StartCoroutine(FinalizarAtaque());

                PlaySound(attackClip);
            }
        }
    }

    private IEnumerator FinalizarAtaque()
    {
        yield return new WaitForSeconds(1.0f);
        ani.SetBool("attack", false);
        atacando = false;
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource == null || clip == null) return;

        if (currentClip != clip)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
            currentClip = clip;
        }
    }

    private void Update()
    {
        if (!isDead)  //También se revisa antes de cada frame, esto podria consumir mucha memoria? consultarlo despues
        {
            Comportamiento_Enemigo();
        }
    }
}
