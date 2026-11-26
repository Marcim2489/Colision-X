using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]protected Rigidbody2D rb;
    [SerializeField]protected Animator animator;
    [SerializeField]protected Collider2D[] colliders;
    [SerializeField]float explosionDuration = 0.2f;
    [SerializeField]ParticleSystem particleS;
    [SerializeField]bool stopWhenExploding = true;
    [SerializeField]bool explodeOnHit = false;

    public int Damage {get; set;}

    protected bool exploded = false;

    public virtual void Shoot(Vector2 direction, float speed, int damage, float lifetime = 3f)
    {
        Damage = damage;
        rb.linearVelocity = direction * speed;
        transform.up = direction;
        if (lifetime > 0f)
        {
            StartCoroutine(ExplodeCoroutine(lifetime));
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<HealthComponent>(out HealthComponent hc))
        {
            if (collision.gameObject.CompareTag(gameObject.tag) == false)
            {
                hc.ChangeHealth(-Damage);
                OnHit();
            }
        }
    }

    protected IEnumerator ExplodeCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        if (exploded == false)
        {
            Explode();
        }
    }

    protected void Explode()
    {
        exploded = true;
        if (stopWhenExploding)
        {
            rb.linearVelocity = Vector2.zero;
        }
        
        DisableCollisions();
        if (particleS != null)
        {
            particleS.Pause();
        }
        if (animator != null)
        {
            animator.Play("Exploding");
            // animator.sta
            Destroy(gameObject, explosionDuration);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnHit()
    {
        if (explodeOnHit)
        {
            Explode();
        }
    }

    public void EnableCollisions()
    {
        foreach(Collider2D collider in colliders)
        {
            collider.enabled = true;
        }
    }

    public void DisableCollisions()
    {
        foreach(Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
    }
}
