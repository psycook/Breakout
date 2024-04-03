using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    [SerializeField]
    [Range(1, 5)]
    private int HitsToDie = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"I am a {gameObject.tag} and I've been hit by a {collision.gameObject.tag}");

        if(--HitsToDie <= 0)
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
        yield return new WaitForSeconds(0.05f); 
        renderer.color = originalColor; 
    }
}
