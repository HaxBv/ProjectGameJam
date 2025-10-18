using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entity
{
    
    public GameObject Player;
    public float rangoPersecucion = 5f;
    public Rigidbody2D rb;

    public bool EnemyVision = false;
   

    protected virtual void Start()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            if (Player == null)
            {
                Debug.LogError("No se encontr� un objeto con la tag 'Player'");
            }
        }

        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }
}
