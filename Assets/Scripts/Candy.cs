using UnityEngine;
using UnityEngine.Audio;

public class Candy : MonoBehaviour
{
    public AudioClip Collect;
    public float volume = 1f;
    public int CandyValue = 1;

    private CircleCollider2D circleCollider;

    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                PlayCollectSound();
                player.CollectCandy(CandyValue);
                Destroy(gameObject);
            }
        }
    }

    private void PlayCollectSound()
    {
        if (Collect != null)
        {
            AudioSource.PlayClipAtPoint(Collect, transform.position, volume);
        }
    }

    private void OnDrawGizmos()
    {
        if (circleCollider == null)
        {
            circleCollider = GetComponent<CircleCollider2D>();
        }

        if (circleCollider != null)
        {
            Gizmos.color = Color.cyan;

            Vector3 worldPosition = transform.position + (Vector3)circleCollider.offset;
            Gizmos.DrawWireSphere(worldPosition, circleCollider.radius);
        }
    }
}