using UnityEngine;

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
    }
    public void PlayerDetected()
    {


        if (Player == null) return;
        float distanciaJugador = Vector2.Distance(Player.transform.position, transform.position);

        if (distanciaJugador < rangoPersecucion)
            EnemyVision = true;
        else
            EnemyVision = false;
    }

    public void UpdateStatus()
    {
        if (EnemyVision == true)
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
       

        Vector2 direccion = (Player.transform.position - transform.position).normalized;
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
    }

    
}
