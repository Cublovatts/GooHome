using UnityEngine;

public class Pupil : MonoBehaviour
{
    [SerializeField]
    private Player Player;

    private Rigidbody2D _rigidbody;
    private InputController _controller;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _controller = Player.controller;
    }

    void Update()
    {
        if (_controller.IsDragging())
        {
            _rigidbody.AddForce(_controller.dir * 2.0f);
        }
    }
}
