using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PhantomCat : Enemy, ISeguimiento
{
    void Update()
    {
        SeguirPlayer();
    }

    public void SeguirPlayer()
    {
        if (Player == null)
            return;

        float distanciaJugador = Vector2.Distance(Player.transform.position, transform.position);

        Vector2 direccion = (Player.transform.position - transform.position).normalized;


        if (direccion.x != 0)
        {
            FlipSprite(Mathf.Sign(direccion.x));
        }


        Vector2 movimiento = direccion * NormalSpeed * Time.deltaTime;
        transform.position += (Vector3)movimiento;
    }


    private void FlipSprite(float direccionX)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direccionX;
        transform.localScale = scale;
    }
}