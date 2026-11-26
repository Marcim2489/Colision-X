using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]Projectile[] obstacles;
    [SerializeField]float maxSpawnTime = 2f;
    [SerializeField]float minSpawnTime = 1.5f;
    [SerializeField]float obstacleMoveSpeed = 0.2f;
    [SerializeField]float horizontalRange = 2.7f;
    [SerializeField]float timeForMaxDifficulty = 30f;
    [SerializeField]float difficultyFactor = 0.6f;

    float timer = 0;

    void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    void Update()
    {
        if (timer >= timeForMaxDifficulty)
        {
            timer = timeForMaxDifficulty;
            return;
        }
        timer+= Time.deltaTime;
        
    }

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnTime* (1-(difficultyFactor * timer/timeForMaxDifficulty)),
        maxSpawnTime* (1-(difficultyFactor * timer/timeForMaxDifficulty))));

        Projectile p = Instantiate(obstacles[Random.Range(0, obstacles.Length)]);
        p.transform.position = transform.position + (Vector3.right * Random.Range(-horizontalRange, horizontalRange));
        p.Shoot(Vector2.down, obstacleMoveSpeed, 1, 10f);
        StartCoroutine(SpawnCoroutine());
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position + Vector3.left * horizontalRange, transform.position + Vector3.right * horizontalRange);
    }
}
