using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class OverlayUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text TextScore;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Score", 0);
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
        TextScore.text = PlayerPrefs.GetInt("Score").ToString();
    }
}
