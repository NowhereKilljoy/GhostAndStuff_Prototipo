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
    
    private void Start()
    {
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
        gameObject.SetActive(false);
    }
    
    IEnumerator WaitEnd()
    {
        yield return new WaitForSeconds(1f);
        AnimEnd();
    }
}
