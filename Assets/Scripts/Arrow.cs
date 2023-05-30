using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Arrow : MonoBehaviour
{
    LineRenderer arrow;

    public void SetPoint(int index, Vector3 position)
    {
        arrow.SetPosition(index, position);
    }

    public void SetStart(Vector3 position)
    {
        SetPoint(0, position);
    }

    public void SetEnd(Vector3 position)
    {
        SetPoint(1, position);
    }

    public void SetVisible(bool visible)
    {
        arrow.enabled = visible;
    }

    void Start()
    {
        arrow = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // arrow.endWidth = Mathf.Abs((arrow.GetPosition(1) - arrow.GetPosition(0)).magnitude);
    }
}
