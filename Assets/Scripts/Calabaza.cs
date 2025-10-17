using UnityEngine;

public class Calabaza : Enemy, ISeguimiento
{
    private bool numeroGenerado = false;  
    private bool transformado = false;   

    void Update()
    {
        Surprise();
    }

    public void Surprise()
    {
        float distanciaJugador = Vector2.Distance(Player.transform.position, transform.position);

        if (distanciaJugador < rangoPersecucion)
        {
            
            if (numeroGenerado == false)
            {
                numeroGenerado = true;
                int numeroSeleccionado = Random.Range(1, 11);
                Debug.Log("El n�mero seleccionado es: " + numeroSeleccionado);

                if (numeroSeleccionado <= 3)
                {
                    Debug.Log("Transformaci�n activada");
                    transformado = true;
                }
                else
                {
                    Debug.Log("No hubo transformaci�n");
                }
            }

            if (transformado == true)
            {
                SeguirPlayer();
            }
        }
        else
        {
            
            numeroGenerado = false;
            transformado = false;
        }
    }

    public void SeguirPlayer()
    {
        Vector2 direccion = (Player.transform.position - transform.position).normalized;
        Vector2 movimiento = direccion * MoveSpeed * Time.deltaTime;
        transform.position += (Vector3)movimiento;
    }
}
