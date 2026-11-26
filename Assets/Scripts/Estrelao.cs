using System.Collections;
using UnityEngine;

public class Estrelao : MonoBehaviour
{
    [SerializeField]Projectile sphere;
    [SerializeField]Projectile wave;
    [SerializeField]Projectile self;
    [SerializeField]Sprite wakingUpFace;
    [SerializeField]Sprite agressiveFace;
    [SerializeField]SpriteRenderer spriteRenderer;
    [SerializeField]float moveSpeed = 2f;
    [SerializeField]float rotationSpeed = 6f;
    [SerializeField]float startupTime = 3f;
    [SerializeField]float timeBetweenAttacks = 1.5f;
    [SerializeField]HealthComponent healthComponent;
    [SerializeField]int scoreValue = 100000;
    [SerializeField]float projectileOffset = 1f;
    [SerializeField]float sphereSpeed = 7f;
    [SerializeField]float waveSpeed = 4f;

    void Start()
    {
        self.Shoot(Vector2.down, moveSpeed, 1, 150f);
        transform.up = Vector3.up;
        healthComponent.died+=OnDeath;
        int[] a = {-1, 1};
        rotationSpeed *= a[Random.Range(0, a.Length)];
        StartCoroutine(StartupCoroutine());
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed);
    }

    IEnumerator StartupCoroutine()
    {
        yield return new WaitForSeconds(startupTime * 0.8f);
        spriteRenderer.sprite = wakingUpFace;
        yield return new WaitForSeconds(startupTime * 0.2f);
        spriteRenderer.sprite = agressiveFace;
        StartCoroutine(AttackingCoroutine());
    }

    IEnumerator AttackingCoroutine()
    {
        ShootProjectile(transform.up, sphereSpeed, sphere);
        ShootProjectile(-transform.up, sphereSpeed, sphere);
        ShootProjectile(transform.right, sphereSpeed, sphere);
        ShootProjectile(-transform.right, sphereSpeed, sphere);
        ShootProjectile((transform.up + transform.right).normalized, waveSpeed, wave);
        ShootProjectile((-transform.up + -transform.right).normalized, waveSpeed, wave);
        ShootProjectile((-transform.up + transform.right).normalized, waveSpeed, wave);
        ShootProjectile((transform.up + -transform.right).normalized, waveSpeed, wave);
        yield return new WaitForSeconds(0.2f);
        ShootProjectile(transform.up, sphereSpeed, sphere);
        ShootProjectile(-transform.up, sphereSpeed, sphere);
        ShootProjectile(transform.right, sphereSpeed, sphere);
        ShootProjectile(-transform.right, sphereSpeed, sphere);

        
        yield return new WaitForSeconds(0.2f);
        ShootProjectile(transform.up, sphereSpeed, sphere);
        ShootProjectile(-transform.up, sphereSpeed, sphere);
        ShootProjectile(transform.right, sphereSpeed, sphere);
        ShootProjectile(-transform.right, sphereSpeed, sphere);
        yield return new WaitForSeconds(timeBetweenAttacks);
        StartCoroutine(AttackingCoroutine());
    }

    void ShootProjectile(Vector3 direction, float speed, Projectile type)
    {
        Projectile p = Instantiate(type);
        p.transform.position = transform.position + direction*projectileOffset;
        p.Shoot(direction, speed, 1, 100f);
    }

    void OnDeath()
    {
        ScoreManager.Instance.AddScore(scoreValue);
        Destroy(gameObject);
    }
}