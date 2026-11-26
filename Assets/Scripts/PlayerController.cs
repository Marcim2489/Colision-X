using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]InputAction moveInput;
    [SerializeField]InputAction attackInput;

    [SerializeField]float moveSpeed = 10f;
    [SerializeField]float attackCooldown = 0.1f;
    [SerializeField]Projectile projectilePrefab;
    [SerializeField]Vector2 atttackPointOffset;
    [SerializeField]float projectileSpeed = 14f;
    [SerializeField]HealthComponent healthComponent;
    [SerializeField]Rigidbody2D rb;

    // Vector3 moveDirection;

    float attackCooldownTimer = 0;

    void Start()
    {
        moveInput.Enable();
        attackInput.Enable();
        healthComponent.died += OnDeath;
    }

    void Update()
    {
        if (attackCooldownTimer >= 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        rb.linearVelocity = moveInput.ReadValue<Vector2>() * moveSpeed;

        if (attackInput.IsPressed() && attackCooldownTimer <= 0)
        {
            ShootProjectile();
            attackCooldownTimer = attackCooldown;
        }
    }

    void ShootProjectile()
    {
        Projectile p = Instantiate(projectilePrefab);
        p.transform.position = transform.position + (Vector3)atttackPointOffset;
        p.Shoot(Vector2.up, projectileSpeed, 1);

        Projectile pb = Instantiate(projectilePrefab);
        pb.transform.position = transform.position + (Vector3)(atttackPointOffset * new Vector2(-1f, 1f));
        pb.Shoot(Vector2.up, projectileSpeed, 1);
    }

    void OnDeath()
    {
        // Destroy(gameObject);
        SceneManager.LoadScene("Game Over");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)atttackPointOffset, 0.05f);
        Gizmos.DrawWireSphere(transform.position + (Vector3)(atttackPointOffset * new Vector2(-1f, 1f)), 0.05f);
    }
}
