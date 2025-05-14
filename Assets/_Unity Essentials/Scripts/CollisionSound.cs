using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CollisionSound : MonoBehaviour
{
    public AudioClip collisionSound;
    public float volume = 1.0f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collisionSound != null)
        {
            audioSource.PlayOneShot(collisionSound, volume);
        }
    }
}

