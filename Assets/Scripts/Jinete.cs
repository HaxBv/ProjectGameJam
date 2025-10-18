using UnityEngine;
using UnityEngine.Audio;

public class Jinete : Enemy, ISeguimiento, IDetection, IStatus
{

    private AudioSource Trote;
    private bool isMoving;

    private int direction = 1;
    //private bool isChasing = false;




    void Update()
    {
        UpdateStatus();
        PlayerDetected();
        AudioControl();
    }

    private void DefaultMovement()
    {
        transform.position += Vector3.right * direction * NormalSpeed * Time.deltaTime;
        isMoving = true;
    }


    public void SeguirPlayer()
    {
        if (Player == null)
            return;

        float directionX = Mathf.Sign(Player.transform.position.x - transform.position.x);


        if (direction != (int)directionX)
        {
            direction = (int)directionX;
            FlipSprite();
        }

        Vector2 movimiento = new Vector2(direction, 0);
        transform.position += (Vector3)movimiento * FastSpeed * Time.deltaTime;
        isMoving = true;
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
        isMoving = false;

        if (EnemyVision)
        {
            SeguirPlayer();
        }
        else
        {
            DefaultMovement();
        }
    }

    private void AudioControl()
    {
        if(Trote == null)
        {
            return;
        }

        if(isMoving && !Trote.isPlaying)
        {
            Trote.Play();
        }
        else if (!isMoving && Trote.isPlaying)
        {
            Trote.Stop();
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
