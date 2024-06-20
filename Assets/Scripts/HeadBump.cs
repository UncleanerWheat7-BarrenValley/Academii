using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBump : MonoBehaviour
{
    [SerializeField]
    PlayerController controller;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!controller.isGrounded && collision.CompareTag("Block"))
        {
            collision.transform.parent.GetComponent<Block>().Break();
            controller.resetVelocity();
        }
    }
}
