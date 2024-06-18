using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    private int LightsLit;
    public int requiredLights;

    bool ExitOpen;

    private void Start()
    {
        CheckExit();
    }

    void OnEnable()
    {
        LightFlame.onLight += IncreaseLightsLit;
    }
    void OnDisable()
    {
        LightFlame.onLight -= IncreaseLightsLit;
    }

    void IncreaseLightsLit() 
    {
        LightsLit++;
        CheckExit();
    }

    public void CheckExit()
    {
        if (LightsLit == requiredLights)
        {
            animator.SetTrigger("Open");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ExitOpen)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("You Win");
            }
        }
    }
}
