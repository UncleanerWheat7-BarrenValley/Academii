using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    PlayerController playerController;

    private void Update()
    {
        PlayWalk(playerController.currentSpeed);
    }

    public void PlayWalk(float value)
    {
        animator.SetFloat("Walk", value);
    }

    public void PlayJump() 
    {
        animator.SetBool("Jump", true);
    }

    public void PlayLand()
    {
        animator.SetBool("Jump", false);
        animator.SetTrigger("Land");
    }

    public void PlayFire()
    {
        animator.SetTrigger("Fire");
    }
}
