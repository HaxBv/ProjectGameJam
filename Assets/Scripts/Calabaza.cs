using UnityEngine;

public class Calabaza : Enemy, ISeguimiento, IDetection, IStatus
{
    private bool numeroGenerado = false;
    private bool transformado = false;
    public float RangoNoMovement;

    void Update()
    {
        PlayerDetected();
        UpdateStatus();
    }

    public void Surprise()
    {
        
        if (!numeroGenerado)
        {
            numeroGenerado = true;

            int numeroSeleccionado = Random.Range(1, 11);
            Debug.Log("El número seleccionado es: " + numeroSeleccionado);

            if (numeroSeleccionado <= 4)
            {
                Debug.Log("Transformación activada");
                transformado = true;
            }
            else
            {
                Debug.Log("No hubo transformación");
            }
        }

       
        if (transformado)
        {
            SeguirPlayer();
        }
    }

    public void SeguirPlayer()
    {
        if (Player == null)
            return;

        Vector2 direccion = (Player.transform.position - transform.position).normalized;
        Vector2 movimiento = direccion * NormalSpeed * Time.deltaTime;
        transform.position += (Vector3)movimiento;
    }

    public void PlayerDetected()
    {
        if (Player == null)
            return;

        float distanciaJugador = Vector2.Distance(Player.transform.position, transform.position);

        
        if (distanciaJugador < rangoPersecucion)
        {
            EnemyVision = true;
        }
        else
        {
            
            EnemyVision = false;
        }

       
        if (distanciaJugador > RangoNoMovement)
        {
            DefaultMovement();
            
        }
    }

    public void DefaultMovement()
    {
        
        numeroGenerado = false;
        transformado = false;
    }

    public void UpdateStatus()
    {
        if (EnemyVision== true)
        {
            Surprise();
        }
        else
        {
            
        }
    }
}
