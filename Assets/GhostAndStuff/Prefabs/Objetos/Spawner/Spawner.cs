using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo
    public Transform[] spawnPoints; // Puntos donde aparecerán los enemigos
    public int maxEnemies = 4; // Número máximo de enemigos
    public float spawnDelay ; // Tiempo entre cada spawn

    private List<GameObject> activeEnemies = new List<GameObject>(); // Lista de enemigos activos

    void Start()
    {
        // this.gameObject.SetActive(false);
        StartCoroutine(SpawnLoop()); // Iniciar el ciclo de spawn automático
    }

    private IEnumerator SpawnLoop()
    {
        while (true) // Bucle infinito para generar enemigos
        {
            yield return new WaitForSeconds(spawnDelay);
            if (activeEnemies.Count < maxEnemies)
            {
                Debug.Log("sapwneando");
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        activeEnemies.Add(newEnemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
            Destroy(enemy);
        }
    }
}