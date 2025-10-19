using UnityEngine;

public class Calabaza : Enemy, ISeguimiento, IDetection, IStatus
{
    private bool numeroGenerado = false;
    private bool transformado = false;
    public float RangoNoMovement;

    private Vector3 posicionInicial; // Guarda la posición original de la calabaza
    private bool regresando = false; // Nuevo: indica si está volviendo a su sitio


    protected override void Start()
    {
        base.Start();
        posicionInicial = transform.position;
        DefaultMovement();
    }

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

        regresando = false; // deja de regresar

        Vector2 direccion = (Player.transform.position - transform.position).normalized;

        // 🔹 Gira el sprite según la dirección en X
        if (direccion.x != 0)
        {
            FlipSprite(Mathf.Sign(direccion.x));
        }

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

        // 🔹 Si el jugador se aleja mucho, desactiva persecución y comienza a regresar
        if (distanciaJugador > RangoNoMovement)
        {
            DefaultMovement();
            regresando = true;
        }
    }

    public void DefaultMovement()
    {
        numeroGenerado = false;
        transformado = false;
    }

    public void UpdateStatus()
    {
        if (EnemyVision)
        {
            Surprise();
        }
        else if (regresando)
        {
            // 🔹 Regresa a su posición original solo si está lejos
            transform.position = Vector3.MoveTowards(transform.position, posicionInicial, NormalSpeed * Time.deltaTime);

            float dir = posicionInicial.x - transform.position.x;
            if (Mathf.Abs(dir) > 0.1f)
            {
                FlipSprite(Mathf.Sign(dir));
            }

            // 🔹 Si ya llegó a su posición, deja de regresar
            if (Vector3.Distance(transform.position, posicionInicial) < 0.1f)
            {
                regresando = false;
            }
        }
    }

    private void FlipSprite(float direccionX)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direccionX;
        transform.localScale = scale;
    }
}
