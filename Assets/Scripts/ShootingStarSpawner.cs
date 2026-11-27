using UnityEngine;
using System.Collections;

public class ShootingStarSpawner : MonoBehaviour
{
    [SerializeField]Projectile shootingStar;
    [SerializeField]float maxSpawnTime = 28f;
    [SerializeField]float minSpawnTime = 22f;
    [SerializeField]float horizontalOffset = 7f;
    [SerializeField]float verticalRange = 4.7f;
    [SerializeField]float shootingStarSpeed = 7f;

    void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

        Projectile p = Instantiate(shootingStar);
        int[] a = {-1, 1};
        int randomDir = a[Random.Range(0, a.Length)];
        p.transform.position = transform.position + (Vector3.up * Random.Range(0, verticalRange)) + 
        (Vector3.right * randomDir * horizontalOffset);

        p.Shoot(Vector2.left * randomDir, shootingStarSpeed, 1, 15f);
        if (randomDir > 0)
        {
            p.PSystem.transform.Rotate(Vector3.right * -180);
        }
        
        StartCoroutine(SpawnCoroutine());
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position + (Vector3.right * horizontalOffset), 
        transform.position + (Vector3.right * horizontalOffset) + (Vector3.up * verticalRange));

        Gizmos.DrawLine(transform.position + (Vector3.left * horizontalOffset), 
        transform.position + (Vector3.left * horizontalOffset) + (Vector3.up * verticalRange));
    }
}
