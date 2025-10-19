using UnityEngine;

public class Calabaza : Enemy, ISeguimiento, IDetection, IStatus
{
    private bool numeroGenerado = false;
    private bool transformado = false;
    public float RangoNoMovement;

    private Vector3 posicionInicial;
    private bool regresando = false;

    [Header("Referencias")]
    public Animator animator;

    protected override void Start()
    {
        base.Start();
        posicionInicial = transform.position;

        if (animator == null)
            animator = GetComponent<Animator>();
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
                transformado = true;
               
                animator.Play("CalabazaTransform");
                Debug.Log("Transformación activada");
            }
            else
            {
                transformado = false;
               
                Debug.Log("No hubo transformación");
            }
        }
    }

    public void SeguirPlayer()
    {
        if (Player == null || regresando)
            return;

        

        Vector2 direccion = (Player.transform.position - transform.position).normalized;

        if (direccion.x != 0)
            FlipSprite(Mathf.Sign(direccion.x));

        Vector2 movimiento = direccion * NormalSpeed * Time.deltaTime;
        transform.position += (Vector3)movimiento;
    }

    public void PlayerDetected()
    {
        if (Player == null)
            return;

        float distanciaJugador = Vector2.Distance(Player.transform.position, transform.position);

        if (!regresando && distanciaJugador < rangoPersecucion)
        {
            EnemyVision = true;
        }
        else
        {
            if (EnemyVision)
            {
                numeroGenerado = false;
                transformado = false;
                
                animator.Play("CalabazaUntransform");
            }
            EnemyVision = false;
        }

        if (distanciaJugador > RangoNoMovement)
        {
            regresando = true;
            EnemyVision = false;
        }
    }

    public void UpdateStatus()
    {
        if (regresando)
        {
            RegresarAlOrigen();
        }
        else if (EnemyVision)
        {
            if (!numeroGenerado)
                Surprise();

            if (transformado)
                SeguirPlayer();
        }
        else
        {
            
        }
    }

    private void RegresarAlOrigen()
    {
       

        transform.position = Vector3.MoveTowards(transform.position, posicionInicial, FastSpeed * Time.deltaTime);

        float dir = posicionInicial.x - transform.position.x;
        if (Mathf.Abs(dir) > 0.1f)
            FlipSprite(Mathf.Sign(dir));

        if (Vector3.Distance(transform.position, posicionInicial) < 0.1f)
        {
            regresando = false;
            numeroGenerado = false;
            transformado = false;
            EnemyVision = false;

            
            animator.Play("CalabazaIdle");
        }
    }

    private void FlipSprite(float direccionX)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direccionX;
        transform.localScale = scale;
    }
}
