using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UFOFlightController : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float mouseSensitivity = 2f;

    private Rigidbody rb;
    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;

        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Start with current rotation
        Vector3 currentRotation = transform.localEulerAngles;
        rotationX = currentRotation.y;
        rotationY = -currentRotation.x;
    }

    void Update()
    {
        // Mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX += mouseX;
        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, -80f, 80f); // Limit pitch

        // Apply rotation
        transform.rotation = Quaternion.Euler(rotationY, rotationX, 0f);

        // Move forward constantly when input is pressed
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

        // Optional: Add backward/strafe movement if desired
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * moveSpeed * Time.deltaTime;
        }

        // Vertical movement
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += transform.up * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.position -= transform.up * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
