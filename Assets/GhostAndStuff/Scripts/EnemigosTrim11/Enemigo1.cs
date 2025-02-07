using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo1 : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator ani;
    public Quaternion angulo;
    public float grado;

    //objetivo que detectara/perseguira el enemigo
    public GameObject target;

    public NavMeshAgent agente;
    public float distancia_ataque;
    public float radio_vision;

    void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.Find("PlayerArmature");
    }

    public void Comportamiento_Enemigo()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > radio_vision)
        {
            agente.enabled = false;
            //ani.SetBool("run", false);
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:

                    //ani.SetBool("Walk", false);
                    break;

                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;

                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                   // ani.SetBool("Walk", true);
                    break;
            }
        }
        else
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
           agente.enabled = true;
            agente.SetDestination(target.transform.position);
            if (Vector3.Distance(transform.position, target.transform.position) > distancia_ataque) ;
        }

    }


       
    

    
     
    


private void Update()
    {
        Comportamiento_Enemigo();
    }
}
