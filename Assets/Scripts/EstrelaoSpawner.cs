using System.Collections;
using UnityEngine;

public class EstrelaoSpawner : MonoBehaviour
{
    [SerializeField]Estrelao estrelao;
    [SerializeField]float spawnTime = 40f;
    [SerializeField]float minSpawnTime = 16f;
    [SerializeField]float spawnDecrease = 2f;

    void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(spawnTime);
        Estrelao e = Instantiate(estrelao);
        int[] a ={-1,1};
        e.transform.position = transform.position + (2f * Vector3.right * a[Random.Range(0, a.Length)]);

        spawnTime -= spawnDecrease;
        if (spawnTime <= minSpawnTime)
        {
            spawnTime = minSpawnTime;
        }

        StartCoroutine(SpawnCoroutine());
    }
}

