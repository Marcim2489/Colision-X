using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    [SerializeField]float moveSpeed = 2f;
    [SerializeField]SpriteRenderer spriteRenderer;

    float distanceMoved = 0f;
    float distanceThisFrame;

    void Start()
    {
        // distanceMoved = startingPosition - transform.position.y;
    }

    void Update()
    {
        if (distanceMoved >= spriteRenderer.size.y/2f * transform.localScale.y)
        {
            transform.position += Vector3.up * distanceMoved;
            distanceMoved = 0f;
        }
        distanceThisFrame = moveSpeed * Time.deltaTime;
        transform.position += Vector3.down * distanceThisFrame;
        distanceMoved += distanceThisFrame;
        
    }
}
