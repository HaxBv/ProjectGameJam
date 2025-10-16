using UnityEngine;

public class Enemy : Entity
{
    public GameObject jugador;
    public float rangoPersecucion;
    public Rigidbody2D rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(jugador == null)
        {
            Debug.LogWarning("No existe un juegador");

        }
    }

    // Update is called once per frame
    void Update()
    {
        float distanciaJugador = Mathf.Abs(jugador.transform.position.x - transform.position.x);

        if(distanciaJugador < rangoPersecucion)
        {
            float directionX = Mathf.Sign(jugador.transform.position.x - transform.position.x);
            Vector2 movimiento = new Vector2(directionX, 0) * MoveSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movimiento);

        }
    }
}
