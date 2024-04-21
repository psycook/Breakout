using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

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
    private LevelBehaviour _levelBehaviour;

    private BallBehaviour _ballBehaviour;

    private PlayerInput _playerInput;
    private InputAction _buttonAction;

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
        _ballBehaviour = FindAnyObjectByType<BallBehaviour>();
        _playerInput = GetComponent<PlayerInput>();
        _buttonAction = _playerInput.actions["Buttons"];
        if(_buttonAction != null)
        {
            _buttonAction.Enable();
            _buttonAction.performed += ButtonPressed;
        }

        if (livesText != null)
        {
            livesText.text = $"LIVES\n{lives.ToString("D2")}";
        }
        if (scoreText != null)
        {
            scoreText.text = $"SCORE\n{score.ToString("D6")}";
        }
        if (levelText != null && _levelBehaviour != null)
        {
            _levelBehaviour.newGame();
            levelText.text = $"LEVEL\n{_levelBehaviour.getDisplayLevel().ToString("D2")}";
        }

        gameState = GameState.Idle;
        StartLevel();
        gameState = GameState.Serving;
    }

    private void ButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log($"ButtonPressed {context.control.name}");

        if(
            (context.control.name == "buttonSouth" ||
             context.control.name == "space") &&
            gameState == GameState.Serving)
        {
            gameState = GameState.Playing;
            BallBehaviour ballBehaviour = FindAnyObjectByType<BallBehaviour>();
            ballBehaviour.Reset();
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
        if(_levelBehaviour != null)
        {
            _levelBehaviour.startLevel();
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
    private void OnDestroy()
    {
        if (_buttonAction != null)
        {
            _buttonAction.performed -= ButtonPressed;
            _buttonAction.Disable();
        }
    }

    public void levelWon()
    {
        if(_levelBehaviour != null)
        {
            _levelBehaviour.nextLevel();
            levelText.text = $"LEVEL\n{_levelBehaviour.getDisplayLevel().ToString("D2")}";
        }
        gameState = GameState.Idle;
        _ballBehaviour.Reset();
        StartLevel();
        gameState = GameState.Serving;
    }
}