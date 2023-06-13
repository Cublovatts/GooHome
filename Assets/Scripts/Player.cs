using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canJumpMidair = false;

    [SerializeField]
    bool isMidair = true; // prefer hasJumped or canJump?
    [SerializeField]
    private float _maxStoredVelocity;
    [SerializeField] 
    private float _minStoredVelocity;
    [SerializeField]
    private float _storedVelocityDegradeRate;
    Rigidbody2D rb;

    [SerializeField]
    private float _storedVelocity;

    public void Jump(Vector2 dir)
    {
        gameObject.transform.parent = null;
        if (canJumpMidair || !isMidair || rb.velocity.magnitude == 0)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddForce(dir * _storedVelocity, ForceMode2D.Impulse);
            _storedVelocity = 0.0f;
            // hasJumped = true;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _storedVelocity = _minStoredVelocity;
    }

    void Start()
    {
        
    }

    void Update()
    {
        _storedVelocity -= _storedVelocityDegradeRate * Time.deltaTime;
        _storedVelocity = Mathf.Clamp(_storedVelocity, _minStoredVelocity, _maxStoredVelocity);
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
        _storedVelocity = rb.velocity.magnitude;
        _storedVelocity = Mathf.Clamp(_storedVelocity, _minStoredVelocity, _maxStoredVelocity);

        gameObject.transform.SetParent(toStickOn.transform);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
