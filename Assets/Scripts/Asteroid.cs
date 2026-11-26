using UnityEngine;

public class Asteroid : Projectile
{
    [SerializeField]HealthComponent healthComponent;
    [SerializeField]float speedMultiplier = 1f;
    [SerializeField]int scoreValue = 1000;
    [SerializeField]bool rotates = true;

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
        base.Explode();
        ScoreManager.Instance.AddScore(scoreValue);
    }
}
