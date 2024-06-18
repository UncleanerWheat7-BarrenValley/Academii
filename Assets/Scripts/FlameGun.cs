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
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }    

    public void FireFlame() 
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePosition.z = 0; // Ensure the z-coordinate is zero since we're in 2D

        // Calculate the direction from the player to the mouse position
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Instantiate the projectile
        GameObject projectile = Instantiate(fireProjectile, fireProjectileTransform.position, Quaternion.identity);

        Destroy(projectile,5);

        // Get the Rigidbody2D component of the projectile and set its velocity
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * speed;
        }
    }
}
