using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Coin : MonoBehaviour, ICollectable
{
    public delegate void CollectCoin();
    public static event CollectCoin onCollect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect();
        }
    }

    public void Collect()
    {
        onCollect();
        Destroy(gameObject);
    }

}
