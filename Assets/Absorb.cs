using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb : MonoBehaviour
{
    Animator animator;
    public GameManager.AbsorbType absorb;

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
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void AnimEnd()
    {
        _GetID.Notify(2);
    }
    
    IEnumerator WaitEnd()
    {
        yield return new WaitForSeconds(1f);
        AnimEnd();
    }
}
