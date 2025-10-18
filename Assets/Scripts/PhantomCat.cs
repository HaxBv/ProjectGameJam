using UnityEngine;
using UnityEngine.Audio;

public class PhantomCat : Enemy, ISeguimiento
{


    void Update()
    {
        SeguirPlayer();
    }

    public void SeguirPlayer()
    {
        if (Player == null)
        {
            return;

        }

        float distanciaJugador = Vector2.Distance(Player.transform.position, transform.position);

       
        Vector2 direccion = (Player.transform.position - transform.position).normalized;
        Vector2 movimiento = direccion * NormalSpeed * Time.deltaTime;
        transform.position += (Vector3)movimiento;
        
    }
}

