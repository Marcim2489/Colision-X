using System.Collections;
using UnityEngine;

public class FogueteSpawner : MonoBehaviour
{
    [SerializeField]FogueteChato foguete;
    [SerializeField]float maxSpawnTime = 13f;
    [SerializeField]float minSpawnTime = 9f;
    [SerializeField]float timeToStart = 6f;
    [SerializeField]float timeBetweenDoubleSpawn = 0.4f;
    [SerializeField]float horizontalRange = 1.8f;

    void Start()
    {
        StartCoroutine(StartupCoroutine());
    }

    IEnumerator StartupCoroutine()
    {
        yield return new WaitForSeconds(timeToStart);
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        SpawnRocket();
        yield  return new WaitForSeconds(Random.Range(0.9f * timeBetweenDoubleSpawn, 1.1f * timeBetweenDoubleSpawn));
        SpawnRocket();
        StartCoroutine(SpawnCoroutine());
    }

    void SpawnRocket()
    {
        FogueteChato f = Instantiate(foguete);
        f.transform.position = transform.position + (Vector3.right * Random.Range(- horizontalRange, horizontalRange));
        if (f.transform.position.x > transform.position.x)
        {
            f.SetCurrentDirection(-1);
        }
        else if (f.transform.position.x < transform.position.x)
        {
            f.SetCurrentDirection(1);
        }
        else
        {
            int[] a = {-1, 1};
            f.SetCurrentDirection(a[Random.Range(0, a.Length)]);
        }
    }
}
