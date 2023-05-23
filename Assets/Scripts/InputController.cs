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
    public float minLength = 0f, maxLength = 1f;
    public bool isInverted = false;

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

        if (Input.GetMouseButtonDown(0))
        {
            start.transform.position = mousePos;
            start.SetActive(true);
        }

        if (Input.GetMouseButton(0))
        {
            end.transform.position = mousePos;
            end.SetActive(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            start.SetActive(false);
            end.SetActive(false);

            player.Jump((isInverted ? -1 : 1) * (end.transform.position - start.transform.position).normalized * 10.0f);
        }
    }
}
