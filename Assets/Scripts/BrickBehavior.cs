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

    void Start()
    {
        GameObject gameObject = GameObject.Find("GameBehaviour");
        if(gameObject != null)
        {
            _gameBehaviour = gameObject.GetComponent<GameBehaviour>();
        }

    }

    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isIndistructable)
        {
            StartCoroutine(FlashBrick());
            return;
        }
        if(_gameBehaviour != null)
        {
            _gameBehaviour.incrementScore(hitScore);
        }
        hitsToDie--;
        StartCoroutine(FlashBrick());
    }

    private IEnumerator FlashBrick()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Color originalColor = renderer.color; 
        renderer.color = Color.white; 
        yield return new WaitForSeconds(0.1f);
        if(renderer != null)
        {
            renderer.color = originalColor;
        }
        if(hitsToDie <= 0)
        {
            Destroy(gameObject);
        }        
    }
}