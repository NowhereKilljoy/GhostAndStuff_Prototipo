using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RESPAWN : MonoBehaviour
{
    public float threshold;

    void FixedUpdate ()
    {
        if (transform.position.y < threshold)
        {
            transform.position = new Vector3(-66.38f, -9.39f, -17.64f);
        }
    }
}
