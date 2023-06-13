using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WALL_TYPE { normal, sticky, slippy, bouncy }

public class Wall : MonoBehaviour
{
    public WALL_TYPE type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb;
        bool hasRb = collision.gameObject.TryGetComponent<Rigidbody2D>(out rb);

        switch (type)
        {
            case WALL_TYPE.sticky:
                if (hasRb)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    rb.transform.SetParent(transform);
                }
                break;

            case WALL_TYPE.slippy:
                break;

            case WALL_TYPE.normal:
            default:
                break;
        }
    }
}
