using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    int score = 0;

    private void Awake()
    {
        int objectCount = FindObjectsOfType<GameSession>().Length;
        if (objectCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        SetLifeText();
        SetScoreText();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1 )
        {
            TakeLife();  
        } else
        {
            ResetGameSession();
        }

    }
    public void IncreaseScore(int points)
    {
        score += points;
        SetScoreText();
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void TakeLife() {
        playerLives -= 1;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        SetLifeText();
    }

    private void SetLifeText()
    {
        livesText.text = playerLives.ToString();
    }

    private void SetScoreText()
    {
        scoreText.text = score.ToString();
    }


}


