using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    // Movement speed of the laser bolt
    public float speed = 40f;
    // Lifetime duration before automatic destruction
    public float lifeTime = 3f;
    
    private Vector3 lastPosition;

    void Start()
    {
        // Scheduled destruction to clean up the scene
        Destroy(gameObject, lifeTime);
        lastPosition = transform.position;
    }

    void Update()
    {
        // Calculate the distance to move this frame
        float moveDistance = speed * Time.deltaTime;
        Vector3 direction = transform.forward;

        // Predictive raycast to detect collisions between frames
        RaycastHit hit;
        if (Physics.Raycast(lastPosition, direction, out hit, moveDistance + 0.1f))
        {
            CheckCollision(hit.collider);
        }

        // Update position and store it for the next frame
        lastPosition = transform.position;
        transform.Translate(Vector3.forward * moveDistance);
    }

    void CheckCollision(Collider other)
    {
        // Identification of the TargetBehaviour script in the hit hierarchy
        TargetBehaviour target = other.GetComponentInParent<TargetBehaviour>();
        
        if (target != null)
        {
            target.OnShot();
            // Immediate removal of the projectile on impact
            Destroy(gameObject);
        }
    }

    // Safety fallback for standard collisions
    private void OnTriggerEnter(Collider other)
    {
        CheckCollision(other);
    }
}