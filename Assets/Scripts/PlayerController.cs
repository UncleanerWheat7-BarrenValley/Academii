using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    private bool isDashing = false;
    private bool isFireing;
    public bool isGrounded;
    bool playerControlActive = true;
    bool canDash;

    private float gravity = 1;


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
        jump.started += Jump;
        jump.canceled += JumpShorten;
    }

    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
        Flip();
    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isJumping = false;
        }

        if (isDashing)
        {
            playerControlActive = false;
            isDashing = false;
            StartCoroutine(DashCoroutine());
        }

        currentSpeed = MathF.Abs(rb.velocity.x);

        if (!isGrounded && playerControlActive)
        {
            rb.gravityScale += 0.05f;
        }

        if (playerControlActive)
        {
            rb.velocity = new Vector2(moveDirection.x * speed, rb.velocity.y);
        }
    }

    IEnumerator DashCoroutine()
    {
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        if (facingRight)
        {
            rb.AddForce(new Vector2(jumpForce, 0), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(-jumpForce, 0), ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(0.33f);
        rb.gravityScale = 1;

        rb.velocity = Vector2.zero;
        playerControlActive = true;
    }

    private void Flip()
    {
        if (moveDirection.x > 0 && !facingRight || moveDirection.x < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (!isGrounded && !canDash) return;
        if (isGrounded)
        {
            Debug.Log("Jumping");
            isJumping = true;
            animController.PlayJump();
        }
        else if(canDash) 
        {
            Debug.Log("Dashing");
            isDashing = true;
            canDash = false;
        }

    }

    private void JumpShorten(InputAction.CallbackContext context)
    {
        if (isGrounded) return;
        if (rb.velocity.y < 0) return;

        Debug.Log("JumpingShorting");
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);

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

    public void resetVelocity()
    {
        rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = true;
            rb.gravityScale = 1;
            animController.PlayLand();
        }

        if (collision.CompareTag("Spring")) 
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 1;
            rb.AddForce(new Vector2(0, jumpForce * 2), ForceMode2D.Impulse);
        }

        if (collision.CompareTag("Kill")) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Block"))
        {
            isGrounded = false;
            canDash = true;
        }
    }


    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
        jump.Disable();
    }


}
