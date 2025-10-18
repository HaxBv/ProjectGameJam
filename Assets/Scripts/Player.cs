using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Player : Entity
{
    public InputSystem_Actions Input;

    public Vector2 MoveInput;

    public void Awake()
    {
        Input = new ();
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
        Input.Enable();

        Input.Player.Move.performed -= OnMove;
        Input.Player.Move.canceled -= OnMove;
        Input.Player.Move.started -= OnMove;
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
    }

    public void MovementMechanic()

    {
        transform.position += (Vector3) MoveInput * MoveSpeed * Time.deltaTime;
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
            Destroy(gameObject);
            Debug.Log("HAS MUERTO");
        }
    }
}
