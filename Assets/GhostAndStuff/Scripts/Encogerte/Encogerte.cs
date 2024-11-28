using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
public class Encogerte: MonoBehaviour
{
    bool grande;
    Animator particle;
    public ThirdPersonController th;
    private StarterAssetsInputs _inputs;

    private void Start()
    {
        th = GetComponent<ThirdPersonController>();
        _inputs = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputs.actionEncoger && !th.isDashing)
        {
            if (grande)
            {

                //StartCoroutine(particula());
                gameObject.transform.position += new Vector3( 0,.5f, 0);
                gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                grande = false;

            }
            else

            {

                //StartCoroutine(particula());
                gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                grande = true;

            }
            _inputs.actionEncoger = false;
        }
    }



}