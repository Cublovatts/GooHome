using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    private float _elapsedTime = 0.0f;
    private bool _isStarted = false;
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (_isStarted)
        {
            _elapsedTime += Time.deltaTime;
            _text.text = _elapsedTime.ToString("0.00");
        }
    }

    [ContextMenu("Start Timer")]
    public void StartTimer()
    {
        _isStarted = true;
    }

    [ContextMenu("Stop Timer")]
    public void StopTimer()
    {
        _isStarted = false;
    }
}
