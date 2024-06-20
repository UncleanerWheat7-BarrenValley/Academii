using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlameGun : MonoBehaviour
{
    public GameObject fireProjectile;
    public float speed;
    [SerializeField]
    Transform fireProjectileTransform;

    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }

    public void FireFlame()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePosition.z = 0;

        Vector2 direction = (mousePosition - transform.position).normalized;
        GameObject flameProjectile = Instantiate(fireProjectile, fireProjectileTransform.position, Quaternion.identity);
        Destroy(flameProjectile, 5);

        Rigidbody2D rb = flameProjectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * speed;
        }
    }
}
