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
    private int level;
    [SerializeField]
    private TextMeshProUGUI livesText;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI levelText;

    // Start is called before the first frame update
    void Start()
    {
        if(livesText != null)
        {
            livesText.text = $"LIVES\n{score.ToString("D2")}";
        }
        if (scoreText != null)
        {
            scoreText.text = $"SCORE\n{score.ToString("D6")}";
        }
        if (levelText != null)
        {
            levelText.text = $"LEVEL\n{score.ToString("D2")}";
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void decrementLife()
    {
        if(--lives == 0)
        {
            //game over
        }
    }

    public void incrementScore(int value)
    {
        score += value;
        if(scoreText != null)
        {
            scoreText.text = $"SCORE\n{score.ToString("D6")}";
        }
    }
}