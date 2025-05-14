using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RoombaMovement3D : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 200f;
    public float turnCooldown = 0.5f;
    public float obstacleDetectionDistance = 1f;

    private Rigidbody rb;
    private bool canTurn = true;
    private bool isTurning = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent rotation due to physics
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // Prevent tunneling through walls
    }

    void FixedUpdate()
    {
        // Check for obstacles in front
        if (!isTurning && Physics.Raycast(transform.position, transform.forward, obstacleDetectionDistance))
        {
            if (canTurn)
            {
                StartCoroutine(TurnRandomly());
            }
        }
        else if (!isTurning)
        {
            // Move forward only if not currently turning
            rb.MovePosition(rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void OnDrawGizmos()
    {
        // Optional: Visualize the raycast in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * obstacleDetectionDistance);
    }

    System.Collections.IEnumerator TurnRandomly()
    {
        canTurn = false;
        isTurning = true;

        // Pick a random Y rotation
        float randomAngle = Random.Range(90f, 180f);
        float direction = Random.value > 0.5f ? 1f : -1f;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, transform.eulerAngles.y + randomAngle * direction, 0);

        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * rotationSpeed / randomAngle;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }

        isTurning = false;
        yield return new WaitForSeconds(turnCooldown);
        canTurn = true;
    }
}
