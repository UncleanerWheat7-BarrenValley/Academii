using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class OverlayUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text TextScore;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("Score");
        TextScore.text = PlayerPrefs.GetInt("Score").ToString();
    }

    void OnEnable()
    {
        Coin.onCollect += IncreaseScore;
    }
    void OnDisable()
    {
        Coin.onCollect -= IncreaseScore;
    }

    void IncreaseScore()
    {
        score += 100;
        PlayerPrefs.SetInt("Score", score);

        if (score > PlayerPrefs.GetInt("HighScore")) 
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        TextScore.text = PlayerPrefs.GetInt("Score").ToString();
    }

    public void Retry() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
