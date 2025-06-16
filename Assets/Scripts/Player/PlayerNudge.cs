using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerNudge : MonoBehaviour
{

    [SerializeField] private float NudgeForce;
    
    private Rigidbody2D _rigidbody;
    private Vector2 _input;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        _rigidbody.AddForce(NudgeForce * _input);
    }
    
    private void OnRotate(InputValue val)
    {
        _input = val.Get<Vector2>().normalized;
    }
}
