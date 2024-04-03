using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 5.0f;
    public AudioClip batHit;
    public AudioClip wallHit;
    public AudioClip ballFail;

    private Rigidbody2D _rigidBody;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rigidBody = GetComponent<Rigidbody2D>();
        Reset();
    }

    private void Reset()
    {
        transform.position = new Vector2(0.0f, -4f);
        float angle = Random.Range(-45f, 45f);
        Vector2 direction = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
        _rigidBody.velocity = direction * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            _audioSource.PlayOneShot(wallHit);
            if (_rigidBody.velocity.x < 0.2 && _rigidBody.velocity.x > -0.2)
            {
                if (_rigidBody.velocity.x < 0)
                {
                    _rigidBody.velocity = new Vector2(1.0f, _rigidBody.velocity.y);
                }
                else
                {
                    _rigidBody.velocity = new Vector2(-1.0f, _rigidBody.velocity.y);
                }
            }
        }

        if (collision.gameObject.tag == "Player")
        {
            _audioSource.PlayOneShot(batHit);
            float positionDifference = transform.position.x - collision.gameObject.transform.position.x;
            Vector2 newDirection = new Vector2(positionDifference*5.0f, _rigidBody.velocity.y).normalized;
            _rigidBody.velocity = newDirection * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "OutOfBounds")
        {
            _audioSource.PlayOneShot(ballFail);
            Reset();
        }
    }
}
