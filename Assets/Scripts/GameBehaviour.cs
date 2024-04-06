using UnityEngine;
using TMPro;

public class GameBehaviour : MonoBehaviour
{
    [SerializeField]
    [Range(1,7)]
    private int lives;
    [SerializeField]
    private int score;
    [SerializeField]
    private TextMeshProUGUI livesText;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI levelText;
    [SerializeField]
    private LevelBehaviour levelBehaviour;

    private GameState _gameState = GameState.Idle;

    public GameState gameState
    {
        get { return _gameState; }
        set
        {
            _gameState = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(livesText != null)
        {
            livesText.text = $"LIVES\n{lives.ToString("D2")}";
        }
        if (scoreText != null)
        {
            scoreText.text = $"SCORE\n{score.ToString("D6")}";
        }
        if (levelText != null && levelBehaviour != null)
        {
            levelBehaviour.newGame();
            levelText.text = $"LEVEL\n{levelBehaviour.getDisplayLevel().ToString("D2")}";
        }

        gameState = GameState.Idle;
        StartLevel();
        gameState = GameState.Serving;

    }

    // Update is called once per frame
    void Update()
    {
        if(gameState == GameState.Serving)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                gameState = GameState.Playing;
                BallBehaviour ballBehaviour = FindAnyObjectByType<BallBehaviour>();
                ballBehaviour.Reset();
            }
        }
    }

    public void BallLost()
    {
        lives--;

        Debug.Log($"Lives {lives}");

        if (livesText != null)
        {
            livesText.text = $"LIVES\n{lives.ToString("D2")}";
        }

        if (lives == 0)
        {
            gameState = GameState.GameOver;
            FindAnyObjectByType<BallBehaviour>().Reset();
        }
        else
        {
            gameState = GameState.Serving;
            FindAnyObjectByType<BallBehaviour>().Reset();
        }
    }

    private void StartLevel()
    {
        if(levelBehaviour != null)
        {
            levelBehaviour.startLevel();
        }
    }

    public void IncrementScore(int value)
    {
        score += value;
        if(scoreText != null)
        {
            scoreText.text = $"SCORE\n{score.ToString("D6")}";
        }
    }

}