using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, ICharacter
{
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    AnimationController animController;
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    FlameGun flameGun;



    public float currentSpeed;
    public float speed = 5;
    public float jumpForce;
    public PlayerInputActions playerControls;
    private InputAction move;
    private InputAction fire;
    private InputAction jump;
    bool facingRight = true;
    private bool isJumping = false;
    private bool isFireing;
    private bool isGrounded;


    Vector2 moveDirection = Vector2.zero;

    float ICharacter.speed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public int Health { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void damage(int damage)
    {
        Health -= damage;
    }

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
        jump = playerControls.Player.Jump;
        jump.Enable();
        jump.performed += Jump;
    }

    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * speed, rb.velocity.y);
        if (isJumping)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isJumping = false;
        }

        currentSpeed = MathF.Abs(rb.velocity.x);
    }

    private void Flip()
    {
        if (rb.velocity.x > 0 && !facingRight || rb.velocity.x < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (!isGrounded) return;

        Debug.Log("Jumping");
        isJumping = true;
        animController.PlayJump();

    }

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Firing");
        isFireing = true;
        animController.PlayFire();
        flameGun.FireFlame();
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.GetChild(0).Rotate(0, 180, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = true;
            animController.PlayLand();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }


    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
        jump.Disable();
    }

    
}
