using UnityEngine;

public class Collectible : MonoBehaviour
{
    private float rotationSpeed = 0.5f;
    public GameObject onCollectEffect;
    public AudioClip collectSound; // assign this in the inspector

    private void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Play sound at collectible's position
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            // Instantiate particle effect
            Instantiate(onCollectEffect, transform.position, transform.rotation);

            // Destroy collectible
            Destroy(gameObject);
        }
    }
}
