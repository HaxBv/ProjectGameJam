using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class Enemy : Entity
{
    
    public GameObject Player;
    public float rangoPersecucion = 5f;
    public Rigidbody2D rb;

    public bool EnemyVision = false;
    protected float CurrentDirectionTimer;
    protected Vector2 CurrentDir;


    protected virtual void Start()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            if (Player == null)
            {
                Debug.LogError("No se encontró un objeto con la tag 'Player'");
            }
        }

        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }
}
