using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public bool canJumpMidair = false;
    [SerializeField]
    bool isMidair = true; // prefer hasJumped or canJump?

    public SaveData data;
    public Transform checkpoints;
    public int checkpointIndex = 0;

    public void Jump(Vector2 dir)
    {
        if (canJumpMidair || !isMidair || rb.velocity.magnitude == 0)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddForce(dir, ForceMode2D.Impulse);
            transform.SetParent(null);
        }
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
        if (collision.tag == "Out of Bounds")
        {
            data.deaths += 1;

            transform.position = checkpoints.GetChild(checkpointIndex).position;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
