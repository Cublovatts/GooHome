using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    public void Jump(Vector2 dir)
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddForce(dir, ForceMode2D.Impulse);
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
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
