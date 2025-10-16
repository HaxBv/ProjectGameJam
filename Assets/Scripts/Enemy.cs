using UnityEngine;

public class Enemy : Entity
{
    public GameObject Player;
    public float rangoPersecucion;
    public Rigidbody2D rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(Player == null)
        {
            Debug.LogWarning("No existe un juegador");
        }
    }

  
    void Update()
    {
        
    }


    

}
