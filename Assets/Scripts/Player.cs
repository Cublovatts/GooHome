using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    [SerializeField]
    private AudioClip _landingSoundEffect;
    private Rigidbody2D rb;
    private AudioSource _audioSource;
    public bool canJumpMidair = false;

    [SerializeField]
    bool isMidair = true; // TODO: change to isGrounded
    // TODO: add isSticking
    [SerializeField]
    private float _maxStoredVelocity;
    [SerializeField] 
    private float _minStoredVelocity;
    [SerializeField]
    private float _storedVelocityDegradeRate;

    [SerializeField]
    private float _storedVelocity;

    public SaveData data;
    public Transform checkpoints;

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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _storedVelocity = rb.velocity.magnitude;
        _storedVelocity = Mathf.Clamp(_storedVelocity, _minStoredVelocity, _maxStoredVelocity);
    
        // TODO: Check for different collider types
        // E.g. if (collider is sticky)
        //rb.constraints = RigidbodyConstraints2D.FreezeAll;
        isMidair = false;
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
        isMidair = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isMidair = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Out of Bounds":
                data.deaths += 1;
                transform.position = checkpoints.GetChild(data.lastCheckpoint).position;
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                break;

            case "Checkpoint":
                data.lastCheckpoint = collision.transform.GetSiblingIndex();
                break;

        }
    }
}
