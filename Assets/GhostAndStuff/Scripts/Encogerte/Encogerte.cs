using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
public class Encogerte: MonoBehaviour
{
    bool grande;
    Animator particle;
    public ThirdPersonController th;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !th.isDashing)
        {
            if (grande)
            {

                //StartCoroutine(particula());
                gameObject.transform.position += new Vector3( 0,.5f, 0);
                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                grande = false;

            }
            else

            {

                //StartCoroutine(particula());
                gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                grande = true;

            }
        }
    }



}