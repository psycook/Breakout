using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private float startSpeed = 5.0f;
    [SerializeField]
    private AudioClip batHit;
    [SerializeField]
    private AudioClip wallHit;
    [SerializeField]
    private AudioClip ballFail;
    [SerializeField]
    private AudioClip brickHit;

    private float _speed;
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
        _speed = startSpeed;
        transform.position = new Vector2(0.0f, -4f);
        float angle = Random.Range(-45f, 45f);
        Vector2 direction = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
        _rigidBody.velocity = direction * _speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Brick")
        {
            if(brickHit != null && _audioSource != null)
            {
                _audioSource.PlayOneShot(brickHit);
            }
            _speed += 0.025f;
        }
        else if (collision.gameObject.tag == "Wall")
        {
            if (wallHit != null && _audioSource != null)
            {
                _audioSource.PlayOneShot(wallHit);
            }
            if(Mathf.Abs(_rigidBody.velocity.x) < 0.25)
            {
                if(_rigidBody.velocity.x >= 0.0f)
                {
                    _rigidBody.velocity = new Vector2(0.25f, _rigidBody.velocity.y);
                }
                else
                {
                    _rigidBody.velocity = new Vector2(-0.25f, _rigidBody.velocity.y);
                }
            }
        }
        else if (collision.gameObject.tag == "Player")
        {
            if (batHit != null && _audioSource != null)
            {
                _audioSource.PlayOneShot(batHit);
            }
            float positionDifference = transform.position.x - collision.gameObject.transform.position.x;
            Vector2 newDirection = new Vector2(positionDifference*5.0f, _rigidBody.velocity.y).normalized;
            _rigidBody.velocity = newDirection * _speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "OutOfBounds")
        {
            if (ballFail != null && _audioSource != null)
            {
                _audioSource.PlayOneShot(ballFail);
            }
            Reset();
        }
    }
}