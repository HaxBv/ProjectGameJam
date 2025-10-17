using UnityEngine;

public class Clayface : Enemy, ISeguimiento
{
    void Update()
    {
        SeguirPlayer();
    }

    public void SeguirPlayer()
    {
        float distanciaJugador = Vector2.Distance(Player.transform.position, transform.position);

        if (distanciaJugador < rangoPersecucion)
        {
            Vector2 direccion = (Player.transform.position - transform.position).normalized;
            Vector2 movimiento = direccion * MoveSpeed * Time.deltaTime;
            transform.position += (Vector3)movimiento; 
        }
    }
}
