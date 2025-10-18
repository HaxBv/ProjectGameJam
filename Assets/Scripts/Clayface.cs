using UnityEngine;

public class Clayface : Enemy, ISeguimiento
{
    public float ChangeDirectionTimer;
    public float CurrentDirectionTimer;
    public Vector2 CurrentDir;
    
    protected override void Start()
    {
        base.Start();

        if(EnemyVision == false)
        {
            DefaultMovement();
        }
    }

    void Update()
    {
        if(EnemyVision == true)
        {
            SeguirPlayer();
        }
        else
        { 
            Timer();
            MoveDirection();
        }
    }

    public void Timer()
    {
        CurrentDirectionTimer += Time.deltaTime;
        if(CurrentDirectionTimer > ChangeDirectionTimer)
        {
            DefaultMovement();
            CurrentDirectionTimer = 0;
        }
    }

    public void SeguirPlayer()
    {
        if (Player == null)
        {
            EnemyVision = false;
            DefaultMovement();
            return;

        }

        float distanciaJugador = Vector2.Distance(Player.transform.position, transform.position);

        if (distanciaJugador < rangoPersecucion)
        {
            EnemyVision = true;

            Vector2 direccion = (Player.transform.position - transform.position).normalized;
            Vector2 movimiento = direccion * MoveSpeed * Time.deltaTime;
            transform.position += (Vector3)movimiento; 
        }
        else
        {
            EnemyVision = false;
            DefaultMovement();
        }
    }

    public void DefaultMovement()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        CurrentDir = new Vector2 (randomX, randomY).normalized;
    }

    public void MoveDirection()
    {
        Vector3 Dir = (Vector3)CurrentDir;

        //Vector3 target = (Vector3)CurrentDir + transform.position;
        //Vector3 Dir = (target - transform.position).normalized;

        transform.position += Dir * MoveSpeed * Time.deltaTime;

    }

}
