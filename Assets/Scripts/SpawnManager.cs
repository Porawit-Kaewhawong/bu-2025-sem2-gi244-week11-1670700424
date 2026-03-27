using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;

    private Coroutine byeRoutine;
    void Start()
    {
        // InvokeRepeating(nameof(RandomSpawn), 0, 3);
        // byeRoutine = StartCoroutine(Bye());
        StartCoroutine(SpawnRoutine());
    }

    void Update()
    {
        if (Time.time > 1)
        {
            // StopCoroutine(byeRoutine);
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            RandomSpawn();
            yield return new WaitForSeconds(3f);
        }
    }

    void RandomSpawn()
    {
        var index = Random.Range(0, spawnPoints.Length);
        var spawnPoint = spawnPoints[index];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

    IEnumerator Hello(float delay)
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("Hello, Frame Count: " + Time.frameCount);
    }

    IEnumerator Bye()
    {
        while (true)
        {
            Debug.Log("Bye, Frame Count: " + Time.frameCount + ", Time: " + Time.time);
            yield return new WaitForSeconds(1f);

            yield return Hello(2f);
            yield return new WaitForSeconds(1f);

            if (Time.time > 5)
            {
                yield break;
            }

        }
    }
}
