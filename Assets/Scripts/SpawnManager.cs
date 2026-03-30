using System.Collections;
using System.Collections.Generic;
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
    public int currentWave = 0;
    public Transform powerUpSpawnArea;
    public GameObject[] powerUps;
    public GameObject enemyPrefab;

    void Start()
    {
        StartCoroutine(WaveControl());
    }

    IEnumerator WaveControl()
    {
        for (int i = 0; i < waves.Count; i++)
        {
            currentWave = i + 1;
            yield return StartCoroutine(WaveSpawn(i));

            Debug.Log($"Wave {currentWave} is complete.");
        }
    }

    IEnumerator WaveSpawn(int waveId)
    {
        Wave waveData = waves[waveId];

        List<int> selectedPoints = new List<int>();

        // Pre-random select spawn points
        for (int i = 0; i < waveData.numberOfRandomSpawnPoint; i++)
        {
            int random;

            do random = Random.Range(0, spawnPoints.Length);
            while (selectedPoints.Contains(random));

            selectedPoints.Add(random);
        }

        // Delay start
        yield return new WaitForSeconds(waveData.delayStart);

        // Spawn power ups
        for (int i = 0; i < waveData.numberOfPowerUp; i++)
        {
            Vector2 randomOffset2D = Random.insideUnitCircle * powerUpSpawnArea.localScale.x * 0.5f;
            Vector3 powerUpSpawnPos = new Vector3(
                powerUpSpawnArea.position.x + randomOffset2D.x,
                powerUpSpawnArea.position.y + 0.5f,
                powerUpSpawnArea.position.z + randomOffset2D.y);

            int random = Random.Range(0, powerUps.Length);
            Instantiate(powerUps[random], powerUpSpawnPos, Quaternion.identity);
        }

        // Spawn enemies using selected spawn points
        for (int i = 0; i < waveData.totalSpawnEnemies; i++)
        {
            int random = Random.Range(0, selectedPoints.Count);
            Instantiate(enemyPrefab, spawnPoints[random].position, Quaternion.identity);

            yield return new WaitForSeconds(waveData.spawnInterval);
        }
    }
}
