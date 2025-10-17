using UnityEngine;

public class Jinete : Enemy, ISeguimiento
{
    
    

    void Update()
    {
        SeguirPlayer();
    }

    /*public void SeguimientoDefault()
    {
        if (PlayerVision == false)
        {
            
            transform.position += Vector2 * MoveSpeed * Time.deltaTime;
        }
    }*/
    public void SeguirPlayer()
    {

        float distanciaJugadorY = Mathf.Abs(Player.transform.position.y - transform.position.y);

        if (distanciaJugadorY < rangoPersecucion)
        {
            

            float directionX = Mathf.Sign(Player.transform.position.x - transform.position.x);

            Vector2 movimiento = new Vector2(directionX, 0);

            transform.position += (Vector3)movimiento * MoveSpeed * Time.deltaTime;

        }
        else
        {
            
        }
    }

    
}
