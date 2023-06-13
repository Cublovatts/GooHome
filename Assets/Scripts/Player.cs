using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private AudioClip _landingSoundEffect;
    private Rigidbody2D rb;
    private AudioSource _audioSource;
    public bool canJumpMidair = false;
    [SerializeField]
    bool isMidair = true; // prefer hasJumped or canJump?

    public SaveData data;
    public Transform checkpoints;

    public void Jump(Vector2 dir)
    {
        if (canJumpMidair || !isMidair || rb.velocity.magnitude == 0)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddForce(dir, ForceMode2D.Impulse);
            transform.SetParent(null);
        }
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: Check for different collider types
        // E.g. if (collider is sticky)
        //rb.constraints = RigidbodyConstraints2D.FreezeAll;
        isMidair = false;
        _audioSource.PlayOneShot(_landingSoundEffect);
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
