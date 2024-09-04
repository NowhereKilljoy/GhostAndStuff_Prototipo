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
            transform.position = new Vector3(-52.3f, 1.5f, -8.060307f);
        }
    }
}
