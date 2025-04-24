using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int maxBalls = 3;
    public GameObject gameOverPanel;

    private int currentBalls;
    private bool isGameOver;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentBalls = maxBalls;
        UpdateBallDisplay();
    }

    public void BallLost()
    {
        currentBalls--;
        UpdateBallDisplay();

        if (currentBalls <= 0)
        {
            GameOver();
        }
        else
        {
            BallLauncher.Instance.ResetLauncher();
        }
    }

    void UpdateBallDisplay()
    {
        // Обновление UI количества шаров
    }

    void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
}