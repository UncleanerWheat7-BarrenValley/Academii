using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    TMP_Text TextScore;

    private void Start()
    {
        TextScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(2);
        PlayerPrefs.SetInt("Score", 0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Tutorial() 
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
