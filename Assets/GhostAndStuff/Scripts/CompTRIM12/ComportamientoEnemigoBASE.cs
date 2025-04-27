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

    //objetivo que detectara/perseguira el enemigo
    public GameObject target;
    //boleano del enemigo atacando
    public bool atacando;
    void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.Find("PlayerArmature");
    }

    public void Comportamiento_Enemigo()
    {
        // distancia a partir de la cual ya no persigue al enemigo 
        if (Vector3.Distance(transform.position, target.transform.position) > 10)
        {
            ani.SetBool("run", false);
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
        else
        {
          if (Vector3.Distance(transform.position, target.transform.position) > 1 && !atacando)
            {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                ani.SetBool("walk", false);

                ani.SetBool("run", true);
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);

                ani.SetBool("attack", false);
            }
            else
            {
                ani.SetBool("walk", false);
                ani.SetBool("run", false);

                ani.SetBool("attack", true);
                atacando = true;


            }
        }


    }

    public void Final_Ani()
    {
        ani.SetBool("attack", false);
        atacando = false;
    
    }








private void Update()
    {
        Comportamiento_Enemigo();
    }
}
