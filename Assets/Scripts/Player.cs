using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class Player : Entity
{
    private AudioSource Pisaditas;
    public InputSystem_Actions Input;
    private int candyCount = 0;
    public Vector2 MoveInput;
    public Animator animator;

    public void Awake()
    {
        Input = new();
        Pisaditas = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Input.Enable();
        Input.Player.Move.performed += OnMove;
        Input.Player.Move.canceled += OnMove;
        Input.Player.Move.started += OnMove;
    }

    private void OnDisable()
    {
        Input.Disable();
        Input.Player.Move.performed -= OnMove;
        Input.Player.Move.canceled -= OnMove;
        Input.Player.Move.started -= OnMove;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    void Update()
    {
        MovementMechanic();
        AudioPisaditas();
        Animation();
    }

    public void MovementMechanic()
    {
        transform.position += (Vector3)MoveInput * NormalSpeed * Time.deltaTime;
    }

    public void Animation()
    {
        float velocidadX = MoveInput.x;
        float velocidadY = MoveInput.y;
        float velocidadTotal = MoveInput.magnitude; 

        if (animator != null)
        {
            animator.SetFloat("Movement", velocidadTotal);
           
        }

       
        if (velocidadX != 0)
            FlipSprite(Mathf.Sign(velocidadX));
    }

    private void FlipSprite(float direccionX)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direccionX;
        transform.localScale = scale;
    }

    public void AudioPisaditas()
    {
        bool IsMoving = MoveInput.sqrMagnitude > 0.1f;
        if (Pisaditas == null) return;

        if (IsMoving && !Pisaditas.isPlaying)
            Pisaditas.Play();
        else if (!IsMoving && Pisaditas.isPlaying)
            Pisaditas.Stop();
    }

    public void CollectCandy(int amount)
    {
        candyCount += amount;
        Debug.Log("Caramelos Recogidos " + candyCount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Debug.Log("HAS MUERTO");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (Pisaditas != null && Pisaditas.isPlaying)
                Pisaditas.Stop();

            Destroy(gameObject);
            Debug.Log("HAS MUERTO");
        }
    }
}
