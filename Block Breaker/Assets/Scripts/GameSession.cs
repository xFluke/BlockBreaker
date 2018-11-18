using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour {

    [SerializeField] bool isAutoPlayEnabled;
    [SerializeField] TextMeshProUGUI scoreText;
    [Range(0.1f, 10f)] [SerializeField] private float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 83;

    private bool isPaused = false;
    private int currentScore = 0;
    private Vector2 currentBallVelocity;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
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
        scoreText.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update ()
    {
        Time.timeScale = gameSpeed;
        CheckForPause();
    }

    private void CheckForPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject pausePanel = FindObjectOfType<Canvas>().transform.Find("Pause Panel").gameObject;

            if (isPaused)
            {
                pausePanel.SetActive(false);
                ResumeGame();
            }
            else
            {
                pausePanel.SetActive(true);
                PauseGame();
            }
        }
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();
    }

    public void ResetGame()
    {
        isPaused = false;
        Destroy(gameObject);
    }

    public void ResetScene()
    {
        isPaused = false;
        GameObject gameOverPanel = FindObjectOfType<Canvas>().transform.Find("Game Over Panel").gameObject;
        gameOverPanel.SetActive(false);
    }

    public bool GetAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }

    public bool GetIsPaused()
    {
        return isPaused;
    }

    public void PauseGame()
    {
        FindObjectOfType<Ball>().stopMoving();
        isPaused = true;
    }

    public void ResumeGame()
    {
        FindObjectOfType<Ball>().continueMoving();
        isPaused = false;
    }

    public void LevelComplete()
    {
        GameObject levelCompletePanel = FindObjectOfType<Canvas>().transform.Find("Level Complete Panel").gameObject;
        levelCompletePanel.SetActive(true);
        FindObjectOfType<Ball>().stopMoving();
    }

    public void RemoveLevelCompletePanel()
    {
        GameObject levelCompletePanel = FindObjectOfType<Canvas>().transform.Find("Level Complete Panel").gameObject;
        if (levelCompletePanel.activeInHierarchy)
        {
            levelCompletePanel.SetActive(false);
        }
    }

    public void EndOfGame()
    {
        RemoveLevelCompletePanel();   
    }
}
