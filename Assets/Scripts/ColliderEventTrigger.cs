using System;
using UnityEngine;
using UnityEngine.Events;

public class ColliderEventTrigger : MonoBehaviour
{
    [Tooltip("The functions to call in OnTriggerEnter2D")] [SerializeField]
    private UnityEvent TriggerEvents;

    [Tooltip("The functions to call in OnCollisionEnter2D")] [SerializeField]
    private UnityEvent CollisionEvents;

    public Collider2D lastTrigger { get; private set; }
    public Collision2D lastCollision { get; private set; }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        lastTrigger = other;
        TriggerEvents.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        lastCollision = collision;
        CollisionEvents.Invoke();
    }
}
