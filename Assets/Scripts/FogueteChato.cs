using UnityEngine;

public class FogueteChato : MonoBehaviour
{
    [SerializeField]Projectile projectile;
    [SerializeField]float verticalMoveSpeed = 4f;
    [SerializeField]float maxHorizontalSpeed = 6f;
    [SerializeField]float horizontalAcceleration = 0.5f;
    [SerializeField]int scoreValue = 10000;
    [SerializeField]HealthComponent healthComponent;
    [SerializeField]Rigidbody2D rb;
    int currentDirection = 1;

    public void SetCurrentDirection(int direction)
    {
        currentDirection = direction;
    }

    void Start()
    {
        healthComponent.died += OnDeath;
        projectile.Shoot(Vector2.down, verticalMoveSpeed, 1, 10);
        transform.up = Vector3.up;
    }

    void FixedUpdate()
    {
        if (projectile.Exploded)
        {
            return;
        }
        rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, maxHorizontalSpeed * currentDirection,
        horizontalAcceleration -(0.7f * horizontalAcceleration * (Mathf.Abs(rb.linearVelocityX)/maxHorizontalSpeed)));
        // Debug.Log(Mathf.Abs(rb.linearVelocityX));
        if (maxHorizontalSpeed - Mathf.Abs(rb.linearVelocityX) <= 0.1f)
        {
            rb.linearVelocityX = maxHorizontalSpeed * currentDirection;
            currentDirection *= -1;
        }
    }

    void OnDeath()
    {
        ScoreManager.Instance.AddScore(scoreValue);
        projectile.Explode();
        rb.linearVelocityX = 0;
        SFXManager.Instance.PlayAudio(SFXManager.Instance.GenericExplosion, transform.position, 0.6f, 0.55f, Random.Range(0.9f, 1.1f));
    }
}
