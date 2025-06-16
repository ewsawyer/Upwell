using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerFreeze : MonoBehaviour
{

    [Tooltip("Speed to drop at once frozen")] [SerializeField]
    private float DropSpeed;

    [Tooltip("Drag when applying airbrake")] [SerializeField]
    private float Damping;
    
    private Rigidbody2D _rigidbody;
    private float _ogDamping;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _ogDamping = _rigidbody.linearDamping;
    }
    
    private void OnFreeze()
    {
        _rigidbody.linearVelocity = Vector2.down * DropSpeed;
    }

    private void OnStartAirbrake(InputValue val)
    {
        if (!val.isPressed)
            return;
        
        _rigidbody.linearDamping = Damping;
    }

    private void OnEndAirbrake(InputValue val)
    {
        if (val.isPressed)
            return;
        
        _rigidbody.linearDamping = _ogDamping;
    }
}
