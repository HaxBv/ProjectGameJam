using UnityEngine;
using UnityEngine.Audio;

public class Jinete : Enemy, ISeguimiento, IDetection, IStatus
{
    private int direction = 1;
    //private bool isChasing = false;

    void Update()
    {
        UpdateStatus();
        PlayerDetected();
    }

    private void DefaultMovement()
    {
        transform.position += Vector3.right * direction * NormalSpeed * Time.deltaTime;
    }

    public void SeguirPlayer()
    {
        if (Player == null)
            return;

        float directionX = Mathf.Sign(Player.transform.position.x - transform.position.x);

        // 🔹 Actualizamos la dirección (solo si cambia)
        if (direction != (int)directionX)
        {
            direction = (int)directionX;
            FlipSprite();
        }

        Vector2 movimiento = new Vector2(direction, 0);
        transform.position += (Vector3)movimiento * FastSpeed * Time.deltaTime;
    }

    public void PlayerDetected()
    {
        if (Player == null)
            return;

        float distanciaJugadorY = Mathf.Abs(Player.transform.position.y - transform.position.y);

        EnemyVision = distanciaJugadorY < rangoPersecucion;
    }

    public void UpdateStatus()
    {
        if (EnemyVision)
        {
            SeguirPlayer();
        }
        else
        {
            DefaultMovement();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Muro"))
        {
            direction *= -1;
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }
}
