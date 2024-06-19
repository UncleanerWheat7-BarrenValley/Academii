using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using System;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    public SpriteRenderer Holders;
    public SpriteRenderer HolderFill;

    private int LightsLit;
    public int requiredLights;

    bool ExitOpen = false;

    private void Start()
    {
        CheckExit();
        UpdateTorchNumber();
    }

    private void UpdateTorchNumber()
    {
        Holders.size = new Vector2(requiredLights * 0.64f, 1.28f);
        HolderFill.size = new Vector2(0, 1.28f);
        HolderFill.transform.localPosition += Vector3.left * 0.32f;

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
        HolderFill.size = new Vector2(LightsLit * 0.64f, 1.28f);
        CheckExit();
    }

    public void CheckExit()
    {
        if (LightsLit == requiredLights)
        {
            animator.SetTrigger("Open");
            ExitOpen = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ExitOpen)
        {
            if (collision.CompareTag("Player"))
            {
                if (SceneManager.GetActiveScene().name == "Tutorial") 
                {
                    SceneManager.LoadScene(0);
                }
                Debug.Log("You Win");
                if (SceneManager.sceneCount < SceneManager.GetActiveScene().buildIndex + 1)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
    }
}
