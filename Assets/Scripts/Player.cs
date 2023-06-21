using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public SaveData Data;
    public Transform Checkpoints;
    public bool CanJumpMidair = false;

    [SerializeField]
    private AudioClip _landingSoundEffect;
    [FormerlySerializedAs("isMidair")]
    [SerializeField]
    bool _isMidair = true;

    private Rigidbody2D _rigidBody;
    private AudioSource _audioSource;
    
    public void Jump(Vector2 dir)
    {
        gameObject.transform.parent = null;
        if (CanJumpMidair || !_isMidair || _rigidBody.velocity.magnitude == 0)
        {
            _rigidBody.constraints = RigidbodyConstraints2D.None;
            _rigidBody.AddForce(dir , ForceMode2D.Impulse);
            transform.SetParent(null);
        }
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isMidair = false;
        _audioSource.PlayOneShot(_landingSoundEffect);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        _isMidair = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _isMidair = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Out of Bounds":
                Data.deaths += 1;
                transform.position = Checkpoints.GetChild(Data.lastCheckpoint).position;
                _rigidBody.velocity = Vector2.zero;
                _rigidBody.angularVelocity = 0f;
                break;

            case "Checkpoint":
                Data.lastCheckpoint = collision.transform.GetSiblingIndex();
                break;

        }
    }
}
