using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RESPAWN_PLAYER : MonoBehaviour
{
    public GameObject Player;
    public Transform spawnPoint;
    public float spawnValue;
    void Update()
    {
        if (Player.transform.position.y < -spawnValue)
        {
            RespawnPoints();
        }
    }

    void RespawnPoints()
    {
        transform.position=spawnPoint.position;
    }
}
