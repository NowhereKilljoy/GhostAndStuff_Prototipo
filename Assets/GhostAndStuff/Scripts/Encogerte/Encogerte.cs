using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Encogerte: MonoBehaviour
{
    bool grande;
    Animator particle;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            if (grande)
            {

                //StartCoroutine(particula());
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