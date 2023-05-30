using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Component references
    Camera camera;

    // Pointer
    public GameObject start, end;
    public GameObject pointer;

    public bool showOnScreen = true;
    public bool isFixedLength = false;
    public float fixedLength = 2f;
    public float minLength = 0f, maxLength = 10f;
    public bool isInverted = false;

    public float force = 1f;

    bool isDragging = false;
    Vector2 dragStart, dragEnd;

    // Player
    public Player player;

    // Arrow
    // public Arrow arrow;

    void Start()
    {
        camera = Camera.main;

        pointer.SetActive(false);
    }

    void Update()
    {
        Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);

        if (isDragging && showOnScreen)
        {
            start.transform.position = dragStart;
            end.transform.position = dragEnd;
            pointer.SetActive(true);
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
            pointer.SetActive(false);
            isDragging = false;
            player.Jump((isInverted ? -1 : 1) * (dragEnd - dragStart) * force);
        }
    }

    private void OnGUI()
    {
        if (isDragging && showOnScreen)
        {
            Vector2 labelPos = dragStart + 0.5f * (dragEnd - dragStart);
            labelPos = camera.WorldToScreenPoint(labelPos);
            labelPos.y = Screen.height - labelPos.y;
            GUI.Label(new Rect(labelPos.x, labelPos.y, 100, 20), string.Format("d={0:N2}", Mathf.Abs((dragEnd - dragStart).magnitude)));
        }
    }
}
