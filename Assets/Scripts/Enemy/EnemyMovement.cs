using System;
using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float Vision;
    [SerializeField] private float FreezePeriod;

    private Transform _player;
    private Rigidbody2D _rigidbody;
    private bool _canMove;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(Freeze());
    }

    private IEnumerator Freeze()
    {
        _canMove = false;
        yield return new WaitForSeconds(FreezePeriod);
        _canMove = true;
    }
    
    private void FixedUpdate()
    {
        if (!_canMove)
            return;
        
        // Check if we can see the player
        float yDist = Mathf.Abs(_player.position.y - transform.position.y);
        if (yDist > Vision)
            return;
        
        // Move towards the player
        Vector2 dir = (_player.position - transform.position).normalized;
        _rigidbody.linearVelocity = Speed * dir;
    }
}
