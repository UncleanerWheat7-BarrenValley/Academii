using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;

    public PlayerInputActions playerControls;
    private InputAction move;
    private InputAction fire;
    private InputAction jump;

    bool facingRight = true;

    Vector2 moveDirection;

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

    private void Update()
    {
        moveDirection = move.ReadValue<Vector2>();

        playerController.MoveCharacter(moveDirection);
        if (moveDirection.x > 0 && !facingRight || moveDirection.x < 0 && facingRight)
        {
            playerController.FlipCharacter();
            facingRight = !facingRight;
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        playerController.Jump();
    }

    private void JumpShorten(InputAction.CallbackContext context)
    {
        playerController.JumpShorten();
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Firing");
        playerController.Fire();
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
        jump.Disable();
    }
}
