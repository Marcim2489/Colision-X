using UnityEngine;

public class ProjectileDisabler : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Projectile>(out Projectile p))
        {
            if (p.CompareTag("ProjetilPlayer"))
            {
                p.DisableCollisions();
                // Debug.Log("a");
            }
        }
    }
}