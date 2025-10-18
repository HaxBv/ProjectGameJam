using UnityEngine;
using UnityEngine.Audio;

public class Candy : MonoBehaviour
{

    public int CandyValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.CollectCandy(CandyValue);
                Destroy(gameObject);
            }
        }

    }
}
