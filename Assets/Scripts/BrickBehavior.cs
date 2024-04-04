using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    [SerializeField]
    [Range(1, 5)]
    private int hitsToDie = 1;
    [SerializeField]
    private Color color = Color.gray;
    [SerializeField]
    private bool isIndistructable = false;


    void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = color;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"I am a {gameObject.tag} and I've been hit by a {collision.gameObject.tag}");

        if(isIndistructable)
        {
            FlashBrick();
            return;
        }

        if(--hitsToDie <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(FlashBrick());
        }
    }

    private IEnumerator FlashBrick()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Color originalColor = renderer.color; 
        renderer.color = Color.white; 
        yield return new WaitForSeconds(0.025f); 
        renderer.color = originalColor; 
    }
}
