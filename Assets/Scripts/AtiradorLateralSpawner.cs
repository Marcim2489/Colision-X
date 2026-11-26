using System.Collections;
using UnityEngine;

public class AtiradorLateralSpawner : MonoBehaviour
{
    [SerializeField]AtiradorLateral atirador;
    [SerializeField]float maxSpawnTime = 13f;
    [SerializeField]float minSpawnTime = 9f;
    [SerializeField]float timeToStart = 6f;
    [SerializeField]float horizontalRange = 2.7f;

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
        AtiradorLateral a = Instantiate(atirador);
        a.transform.position = transform.position + (Vector3.right * Random.Range(- horizontalRange, horizontalRange));
        StartCoroutine(SpawnCoroutine());
    }
}
