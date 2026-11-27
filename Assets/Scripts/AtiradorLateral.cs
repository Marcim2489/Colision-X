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
    [SerializeField]AudioSource laserStartupSFX;
    [SerializeField]AudioSource laserSFX;

    void Start()
    {
        laserA.SetActive(false);
        laserB.SetActive(false);
        healthComponent.died += OnDeath;
        projectile.Shoot(Vector2.down, moveSpeed, 1, 200);
        transform.up = Vector3.up;
        StartCoroutine(ShootCoroutine());
        laserSFX.Stop();
        laserStartupSFX.Stop();
    }

    void Update()
    {
        if (PlayerController.Player == null)
        {
            return;
        }
        laserSFX.transform.position = new Vector3(PlayerController.Player.transform.position.x, laserSFX.transform.position.y);
    }

    IEnumerator ShootCoroutine()
    {
        if (projectile.Exploded)
        {
            yield break;
        }
        yield return new WaitForSeconds(timeToShoot);
        if (projectile.Exploded)
        {
            yield break;
        }
        // cabecaLaserA.SetActive(true);
        // cabecaLaserB.SetActive(true);
        laterais.Play("lateraisIniciandoTiro");
        laserStartupSFX.Play();
        yield return new WaitForSeconds(shootStartup);
        if (projectile.Exploded)
        {
            yield break;
        }
        laserSFX.Play();
        laserA.SetActive(true);
        laserB.SetActive(true);
    }


    void OnDeath()
    {
        ScoreManager.Instance.AddScore(scoreValue);
        laterais.gameObject.SetActive(false);
        laserA.SetActive(false);
        laserB.SetActive(false);
        projectile.Explode();
        SFXManager.Instance.PlayAudio(SFXManager.Instance.GenericExplosion, transform.position, 0.6f, 0.55f);
        // Destroy(gameObject);
    }
}
