using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Player {get; private set;}
    [SerializeField]InputAction moveInput;
    [SerializeField]InputAction attackInput;

    [SerializeField]float moveSpeed = 10f;
    [SerializeField]float attackCooldown = 0.1f;
    [SerializeField]Projectile projectilePrefab;
    [SerializeField]Vector2 atttackPointOffset;
    [SerializeField]float projectileSpeed = 14f;
    [SerializeField]HealthComponent healthComponent;
    [SerializeField]Rigidbody2D rb;
    [SerializeField]Animator animator;
    [SerializeField]float explosionDuration = 0.333f;

    // Vector3 moveDirection;

    float attackCooldownTimer = 0;

    void Awake()
    {
        Player = this;
    }

    void Start()
    {
        moveInput.Enable();
        attackInput.Enable();
        healthComponent.died += OnDeath;
    }

    void Update()
    {
        if (healthComponent.Dead)
        {
            return;
        }
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

        SFXManager.Instance.PlayAudio(SFXManager.Instance.PlayerShoot, Vector3.zero, 0f, 0.3f, Random.Range(0.8f, 1.2f));
    }

    void OnDeath()
    {
        // Destroy(gameObject);
        // animator.Play("Exploding");
        moveInput.Disable();
        attackInput.Disable();
        rb.linearVelocity = Vector2.zero;
        SFXManager.Instance.PlayAudio(SFXManager.Instance.PlayerExplosion, Vector3.zero);
        StartCoroutine(DeathCoroutine());
        // SceneManager.LoadScene("Game Over");
    }

    void OnDestroy()
    {
        Player = null;
    }

    IEnumerator DeathCoroutine()
    {
        animator.Play("Exploding");
        yield return new WaitForSeconds(explosionDuration);
        SceneManager.LoadScene("Game Over");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)atttackPointOffset, 0.05f);
        Gizmos.DrawWireSphere(transform.position + (Vector3)(atttackPointOffset * new Vector2(-1f, 1f)), 0.05f);
    }
}
