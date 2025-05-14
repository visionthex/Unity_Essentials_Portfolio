using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Day Duration Settings")]
    [Tooltip("Duration of a full 360° day cycle in real-world seconds")]
    [Range(10, 86400)] // from 10 seconds to 24 hours
    public float secondsPerDay = 120f;

    private float rotationSpeed;

    void Start()
    {
        // Calculate how many degrees per second based on desired day length
        rotationSpeed = 360f / secondsPerDay;
    }

    void Update()
    {
        // Rotate around the X axis to simulate the sun's movement
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}

