using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForAbsorb : MonoBehaviour
{
    Animator animator;
    public GameManager.AbsorbType absorb;
    public float timeAbsorb = 1.5f;

    [Tooltip("NO USAR mayor que 2")]
    public int keyNumber;

    private EnemyHealth _GetID;
    
    private void Start()
    {
        if (GetComponent<EnemyHealth>() != null)
        {
            _GetID = GetComponent<EnemyHealth>();
        }
        animator = GetComponent<Animator>();
    }

    public void AnimStart()
    {
        animator.SetTrigger("Absorb");
        StartCoroutine(WaitEnd());

    }
    public void AnimInterrupt()
    {
        StopAllCoroutines();
        animator.SetTrigger("Cancelled");

        while(transform.localScale.x < 1)
        {
            transform.localScale += new Vector3(0.1f,0.1f,0.1f);
        }

        transform.localScale = Vector3.one;

    }

    public void AnimEnd()
    {
        if (absorb == GameManager.AbsorbType.Bullet)
        {
            _GetID.Notify(2);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator WaitEnd()
    {
        yield return new WaitForSeconds(timeAbsorb);
        AnimEnd();

        
    }
}
