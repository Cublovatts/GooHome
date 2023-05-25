using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Component references
    Camera camera;

    // Pointer
    public GameObject start, end;
    public bool isFixedLength = false;
    public float fixedLength = 1f;
    public float minLength = 0f, maxLength = float.MaxValue;
    public bool isInverted = false;

    bool isDragging = false;
    Vector2 dragStart, dragEnd;

    // Player
    public Player player;

    // Arrow
    // public Arrow arrow;

    void Start()
    {
        camera = Camera.main;

        start.SetActive(false);
        end.SetActive(false);
    }

    void Update()
    {
        Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);

        if (isDragging)
        {
            start.transform.position = dragStart;
            end.transform.position = dragEnd;

            start.SetActive(true);
            end.SetActive(true);

            Debug.DrawLine(dragStart, dragEnd, Color.white);
        }

        if (Input.GetMouseButtonDown(0))
        {
            dragStart = mousePos;
            isDragging = true;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 dir = (mousePos - dragStart).normalized;

            if (isFixedLength)
            {
                dragEnd = dragStart + fixedLength * dir;
            }
            else
            {
                float length = (mousePos - dragStart).magnitude;
                length = Mathf.Clamp(length, minLength, maxLength);
                dragEnd = dragStart + length * dir;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            start.SetActive(false);
            end.SetActive(false);

            isDragging = false;

            // Jump uses normalized value at the moment, so jump force is constant
            // We want to change this so jump force is directly proportional to magnitude
            // Note: will need to clamp with minimum and maximum values
            player.Jump((isInverted ? -1 : 1) * (end.transform.position - start.transform.position).normalized * 10.0f);
        }
    }

    private void OnGUI()
    {
        if (isDragging)
        {
            Vector2 labelPos = new Vector2(10, 10); // Replace with middle of drag position
            GUI.Label(new Rect(labelPos.x, labelPos.y, 100, 20), string.Format("d={0:N2}", Mathf.Abs((dragEnd - dragStart).magnitude)));
        }
    }
}
