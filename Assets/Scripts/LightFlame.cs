using UnityEngine;

public class LightFlame : MonoBehaviour
{
    public delegate void Lit();
    public static event Lit onLight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Flame"))
        {
            GameObject flame = transform.GetChild(0).gameObject;
            if (!flame.activeSelf)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                onLight();
            }
        }
    }
}
