using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Arrow
    public Arrow arrow;
    public bool isInverted = false;
    public float maxLength = 1f;
    public float endWidth = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if(isInverted)
            {
                arrow.SetEnd(mousePos);
            }
            else
            {
                arrow.SetStart(mousePos);
            }
            
            arrow.SetVisible(true);
        }

        if (Input.GetMouseButton(0))
        {
            if (isInverted)
            {
                arrow.SetStart(mousePos);
            }
            else
            {
                arrow.SetEnd(mousePos);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            arrow.SetVisible(false);
        }
    }
}
