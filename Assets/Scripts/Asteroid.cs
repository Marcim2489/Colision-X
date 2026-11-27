using System.Collections;
using UnityEngine;

public class Asteroid : Projectile
{
    [SerializeField]HealthComponent healthComponent;
    [SerializeField]float speedMultiplier = 1f;
    [SerializeField]int scoreValue = 1000;
    [SerializeField]bool rotates = true;
    [SerializeField]bool shootingStar = false;
    float rotationSpeed;

    void Start()
    {
        healthComponent.died += OnDeath;
        // DisableCollisions();
    }

    public override void Shoot(Vector2 direction, float speed, int damage, float lifetime = 3)
    {
        // base.Shoot(direction, speed * speedMultiplier, damage, lifetime);
        Damage = damage;
        rb.linearVelocity = direction * speed * speedMultiplier;
        // transform.up = direction;
        if (lifetime > 0f)
        {
            StartCoroutine(ExplodeCoroutine(lifetime));
        }
        if (rotates == false)
        {
            return;
        }
        int[] a = {-1, 1};
        rotationSpeed = speed * speedMultiplier * a[Random.Range(0, 2)] * Random.Range(0.8f, 1.2f);
        // rb.angularVelocity = rotationSpeed;
    }

    void Update()
    {
        if (exploded || rotates == false)
        {
            return;
        }
        transform.Rotate(Vector3.forward * rotationSpeed);
    }

    void OnDeath()
    {
        rotationSpeed = 0f;
        Explode();
        if (shootingStar)
        {
            SFXManager.Instance.PlayAudio(SFXManager.Instance.ShootingStarScream, transform.position, 0.5f);
            StartCoroutine(ExplosionSoundCoroutine());
        }
        else
        {
            SFXManager.Instance.PlayAudio(SFXManager.Instance.AsteroidExplosion, transform.position, 0.6f, 0.5f, Random.Range(0.9f, 1.1f));
        }
        ScoreManager.Instance.AddScore(scoreValue);
    }

    IEnumerator ExplosionSoundCoroutine()
    {
        yield return new WaitForSeconds(0.25f);
        SFXManager.Instance.PlayAudio(SFXManager.Instance.ShootingStarExplosion, transform.position, 0.6f, 0.5f);
    }
}
