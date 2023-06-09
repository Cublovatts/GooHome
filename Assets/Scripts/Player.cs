using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public SaveData Data;
    public Transform Checkpoints;
    public bool CanJumpMidair = false;

    [SerializeField]
    bool isMidair = true; // TODO: change to isGrounded
    // TODO: add isSticking
    [SerializeField]
    private float _maxStoredVelocity;
    [SerializeField] 
    private float _minStoredVelocity;
    [SerializeField]
    private float _storedVelocityDegradeRate;

    private AudioClip _landingSoundEffect;
    [FormerlySerializedAs("isMidair")]
    [SerializeField]

    public SaveData data;
    public Transform checkpoints;
    public InputController controller;
    
    private Rigidbody2D _rigidBody;
    private AudioSource _audioSource;

    public Transform contactPoint;
    ParentConstraint pc;

    public void Jump(Vector2 dir)
    {
        if (canJumpMidair || !isMidair || rb.velocity.magnitude == 0)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddForce(dir * _storedVelocity, ForceMode2D.Impulse);
            _storedVelocity = 0.0f;

            pc.constraintActive = false;
            contactPoint.rotation = Quaternion.identity;
        }
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<ParentConstraint>();

        _storedVelocity = _minStoredVelocity;
    }

    void Update()
    {
        _storedVelocity -= _storedVelocityDegradeRate * Time.deltaTime;
        _storedVelocity = Mathf.Clamp(_storedVelocity, _minStoredVelocity, _maxStoredVelocity);

        transform.rotation = new Quaternion(0, 0, 0, 0);

        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isMidair = false;
        _audioSource.PlayOneShot(_landingSoundEffect);

        contactPoint.transform.position = collision.GetContact(0).point;
        contactPoint.SetParent(collision.transform);

        pc.SetTranslationOffset(0, new Vector2(transform.position.x - contactPoint.position.x, transform.position.y - contactPoint.position.y));
        pc.SetRotationOffset(0, new Vector2(transform.position.x - contactPoint.position.x, transform.position.y - contactPoint.position.y));
        pc.constraintActive = true;

        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        rb.transform.SetParent(transform);
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
