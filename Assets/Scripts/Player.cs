using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
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

    public void CollectCandy(int amount)
    {
        candyCount += amount;
        Debug.Log("Caramelos Recogidos " + candyCount);

    }
    private void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }
    void Start()
    {

    }

    void Update()
    {
        MovementMechanic();
        AudioPisaditas();
    }

    public void MovementMechanic()

    {
        transform.position += (Vector3)MoveInput * NormalSpeed * Time.deltaTime;
    }
    public void AudioPisaditas()
    {
        bool IsMoving = MoveInput.sqrMagnitude > 0.1f;
        if(Pisaditas == null)
        {
            return;
        }

        if(IsMoving && !Pisaditas.isPlaying)
        {

            Pisaditas.Play();

        }

        else if (!IsMoving && Pisaditas.isPlaying)
        {

            Pisaditas.Stop();

        }

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
            if(Pisaditas != null & Pisaditas.isPlaying)
            {
                Pisaditas.Stop();
            }

            Destroy(gameObject);
            Debug.Log("HAS MUERTO");
        }
    }
}
