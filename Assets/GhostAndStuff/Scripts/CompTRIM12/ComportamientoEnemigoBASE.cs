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

    void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.Find("PlayerArmature");
    }

    public void Comportamiento_Enemigo()
    {
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
            }
        }
    }

    // Este método se llama automáticamente cuando termina el ataque
    private IEnumerator FinalizarAtaque()
    {
        yield return new WaitForSeconds(1.0f); // Ajusta según duración real de la animación
        ani.SetBool("attack", false);
        atacando = false;
    }

    private void Update()
    {
        Comportamiento_Enemigo();
    }
}