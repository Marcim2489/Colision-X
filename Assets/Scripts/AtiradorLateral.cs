using System.Collections;
using UnityEngine;

public class AtiradorLateral : MonoBehaviour
{
    [SerializeField]Projectile projectile;
    // [SerializeField]GameObject cabecaLaserA;
    // [SerializeField]GameObject cabecaLaserB;
    [SerializeField]Animator laterais;
    [SerializeField]GameObject laserA;
    [SerializeField]GameObject laserB;
    [SerializeField]float timeToShoot = 3;
    [SerializeField]HealthComponent healthComponent;
    [SerializeField]int scoreValue = 10000;
    [SerializeField]float moveSpeed = 3f;
    [SerializeField]float shootStartup = 0.2f;

    void Start()
    {
        laserA.SetActive(false);
        laserB.SetActive(false);
        healthComponent.died += OnDeath;
        projectile.Shoot(Vector2.down, moveSpeed, 1, 200);
        transform.up = Vector3.up;
        StartCoroutine(ShootCoroutine());
    }

    IEnumerator ShootCoroutine()
    {
        yield return new WaitForSeconds(timeToShoot);
        // cabecaLaserA.SetActive(true);
        // cabecaLaserB.SetActive(true);
        laterais.Play("lateraisIniciandoTiro");
        yield return new WaitForSeconds(shootStartup);
        laserA.SetActive(true);
        laserB.SetActive(true);
    }


    void OnDeath()
    {
        ScoreManager.Instance.AddScore(scoreValue);
        Destroy(gameObject);
    }
}
