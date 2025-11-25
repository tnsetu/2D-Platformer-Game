using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pickupClip;

    private bool collected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected)
            return;

        if (!other.CompareTag("Player"))
            return;

        collected = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegisterPickup();
        }

        if (audioSource != null && pickupClip != null)
        {
            audioSource.PlayOneShot(pickupClip);
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collected)
            return;

        if (!collision.gameObject.CompareTag("Player"))
            return;

        collected = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegisterPickup();
        }

        if (audioSource != null && pickupClip != null)
        {
            audioSource.PlayOneShot(pickupClip);
        }

        Destroy(gameObject);
    }
}
