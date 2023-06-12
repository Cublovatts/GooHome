using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public bool canJumpMidair = false;
    [SerializeField]
    bool isMidair = true; // prefer hasJumped or canJump?

    public void Jump(Vector2 dir)
    {
        gameObject.transform.parent = null;
        if (canJumpMidair || !isMidair || rb.velocity.magnitude == 0)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddForce(dir, ForceMode2D.Impulse);
            // hasJumped = true;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
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

    public void Stick(GameObject toStickOn)
    {
        gameObject.transform.SetParent(toStickOn.transform);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
