using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    private int levelCount { get; set; }
    private int levelIndex;
    private int[,] currentLevel;

    [SerializeField]
    private GameObject[] bricks = new GameObject[7];
    [SerializeField]
    private float brickWidth = 0.808f;
    [SerializeField]
    private float brickHeight = 0.404f;

    private static int[,] level1 = new int[10, 20]
    {
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6},
        {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4},
        {3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3},
        {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
    };
    private static int[,] level2 = new int[10, 20]
    {
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
    };

    private int[][,] levels = new int[2][,] { level1, level2 };

    LevelBehaviour()
    {
        newGame();
    }

    public void newGame()
    {
        levelCount = levels.Length;
        levelIndex = 0;
        currentLevel = levels[levelIndex];
    }

    public void setLevel(int newLevelIndex)
    {
        this.levelIndex = newLevelIndex;
    }

    public bool nextLevel()
    {
        if(++levelIndex >= levelCount)
        {
            // game finished
            return false;
        }
        currentLevel = levels[levelIndex];
        return true;
    }

    public int getDisplayLevel()
    {
        return levelIndex + 1;
    }

    public bool startLevel()
    {
        for (int i = 0; i < currentLevel.GetLength(0); i++) // Rows
        {
            for (int j = 0; j < currentLevel.GetLength(1); j++) // Columns
            {
                int brickType = currentLevel[i, j];
                if (brickType != 0)
                {
                    Vector2 position = new Vector2(j * brickWidth, i * -brickHeight);
                    Debug.Log($"Brick position for brick[{j},{i}] is ({position.x},{position.y})");
                    GameObject brick = Instantiate(bricks[brickType - 1], position, Quaternion.identity);
                    brick.transform.SetParent(transform, false);
                }
            }
        }
        return true;
    }
}