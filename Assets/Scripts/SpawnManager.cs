using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public int totalSpawnEnemies;
    public int numberOfRandomSpawnPoint;
    public float delayStart;
    public float spawnInterval;
    public int numberOfPowerUp;
}

public class SpawnManager : MonoBehaviour
{
    public List<Wave> waves;

    public Transform[] spawnPoints;

    public Transform powerUpSpawnArea;
    public GameObject[] powerUps;
    public GameObject enemyPrefab;

    void Start()
    {
        Vector2 offSet2D = Random.insideUnitCircle * powerUpSpawnArea.localScale;
        Debug.Log(offSet2D);

        StartCoroutine(SpawnRoutine());
    }

    void Update()
    {

    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            
        }
    }

    void RandomSpawn(int index)
    {
        var spawnPoint = spawnPoints[index];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
    
    IEnumerator WaveSpawn(int total, int currentWave)
    {
        List<int> selectedPoints = new List<int>();

        // Pre-random select spawn points
        for (int i = 0; i < waves[currentWave].numberOfRandomSpawnPoint; i++)
        {
            int random;

            do random = Random.Range(0, spawnPoints.Length);
            while (selectedPoints.Contains(random));

            selectedPoints.Add(random);
        }

        // Delay start
        yield return new WaitForSeconds(waves[currentWave].delayStart);

        // Spawn power ups
        Vector2 offSet2D = Random.insideUnitCircle * powerUpSpawnArea.localScale;
        Vector3 powerUpSpawnPos = new Vector3();

        for (int i = 0; i < waves[currentWave].numberOfPowerUp; i++)
        {
            int random = Random.Range(0, powerUps.Length);
            Instantiate(powerUps[random], powerUpSpawnPos, Quaternion.identity);
        }

        // Spawn enemies using selected spawn points
        for (int i = 0; i < waves[currentWave].totalSpawnEnemies; i++)
        {
            int random = Random.Range(0, selectedPoints.Count);
            Instantiate(enemyPrefab, spawnPoints[random].position, Quaternion.identity);

            yield return new WaitForSeconds(waves[currentWave].spawnInterval);
        }


    }
}
