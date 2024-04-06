using System.Collections;
using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    [SerializeField]
    [Range(1, 5)]
    private int hitsToDie = 1;
    [SerializeField]
    private int hitScore = 10;
    [SerializeField]
    private bool isIndistructable = false;

    private GameBehaviour _gameBehaviour;
    private Coroutine _flashCoroutine = null;
    private SpriteRenderer _spriteRenderer = null;
    private Color _originalColor;


    void Start()
    {
        GameObject gameObject = GameObject.Find("GameBehaviour");
        if(gameObject != null)
        {
            _gameBehaviour = gameObject.GetComponent<GameBehaviour>();
        }
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isIndistructable && _gameBehaviour != null)
        {
            hitsToDie--;
            _gameBehaviour.IncrementScore(hitScore);
        }
        StartBrickHitCoroutine();
    }

    private void StartBrickHitCoroutine()
    {
        if (_flashCoroutine != null)
        {
            StopCoroutine(_flashCoroutine);
            _spriteRenderer.color = _originalColor;
            _flashCoroutine = null;
        }
        _originalColor = _spriteRenderer.color;
        _spriteRenderer.color = Color.white;
        _flashCoroutine = StartCoroutine(FlashBrick());
    }

    private IEnumerator FlashBrick()
    {
        yield return new WaitForSeconds(0.05f);
        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = _originalColor;
        }
        if (hitsToDie <= 0 && gameObject != null)
        {
            Destroy(gameObject);
            Debug.Log($"There are {FindAnyObjectByType<LevelBehaviour>().decrementLevelBrickCount()} bricks left");
        }
        _flashCoroutine = null;
    }

}