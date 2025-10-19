using UnityEngine;
using UnityEngine.Audio;

public class Clayface : Enemy, ISeguimiento, IDetection, IStatus
{
    public float ChangeDirectionTimer;

    protected override void Start()
    {
        base.Start();
        DefaultMovement();
    }

    void Update()
    {
        PlayerDetected();
        UpdateStatus();
    }

    private void DefaultMovement()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        CurrentDir = new Vector2(randomX, randomY).normalized;

        
        if (CurrentDir.x != 0)
            FlipSprite(Mathf.Sign(CurrentDir.x));
    }

    public void PlayerDetected()
    {
        if (Player == null)
            return;

        float distanciaJugador = Vector2.Distance(Player.transform.position, transform.position);

        EnemyVision = distanciaJugador < rangoPersecucion;
    }

    public void UpdateStatus()
    {
        if (EnemyVision)
        {
            SeguirPlayer();
        }
        else
        {
            Patrullar();
        }
    }

    public void SeguirPlayer()
    {
        if (Player == null)
            return;

        Vector2 direccion = (Player.transform.position - transform.position).normalized;

     
        if (direccion.x != 0)
            FlipSprite(Mathf.Sign(direccion.x));

        Vector2 movimiento = direccion * FastSpeed * Time.deltaTime;
        transform.position += (Vector3)movimiento;
    }

    private void Patrullar()
    {
        Timer();
        MoveDirection();
    }

    private void Timer()
    {
        CurrentDirectionTimer += Time.deltaTime;

        if (CurrentDirectionTimer >= ChangeDirectionTimer)
        {
            DefaultMovement();
            CurrentDirectionTimer = 0f;
        }
    }

    private void MoveDirection()
    {
        Vector3 movimiento = (Vector3)CurrentDir * NormalSpeed * Time.deltaTime;
        transform.position += movimiento;

    
        if (CurrentDir.x != 0)
            FlipSprite(Mathf.Sign(CurrentDir.x));
    }

    private void FlipSprite(float direccionX)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direccionX;
        transform.localScale = scale;
    }
}
