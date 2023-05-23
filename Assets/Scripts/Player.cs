using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Component references
    Rigidbody2D rb;

    public void Jump(Vector2 dir)
    {
        rb.AddForce(dir, ForceMode2D.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
