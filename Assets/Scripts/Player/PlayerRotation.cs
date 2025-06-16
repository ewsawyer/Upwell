using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{

    [Tooltip("Speed in degrees per second that the player can rotate towards the direction of input")] [SerializeField]
    private float Speed;

    [Tooltip("Degrees away from the direction of input where the player will snap to it exactly and not move")]
    [SerializeField]
    private float SnapTolerance;
    
    // [Tooltip("The boxing glove")] [SerializeField]
    // private GloveNoReturn Glove;

    public Vector2 currentDirection
    {
        get => transform.up;
        private set { }
    }

    private Rigidbody2D _rigidbody;
    private PlayerInputHandler _inputHandler;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputHandler = GetComponent<PlayerInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        // No rotation allowed while punching
        // if (Glove.isPunching)
        //     return;
        
        // Figure out which direction to rotate
        float targetAngle = _inputHandler.targetRotation.eulerAngles.z;
        float currentAngle = transform.eulerAngles.z;
        float diff = targetAngle - currentAngle;

        // If we're trying to take the long way around, flip the direction of rotation
        if (Mathf.Abs(diff) > 180.0f)
            diff *= -1.0f;
        
        // Get direction of rotation
        float dir = Mathf.Sign(diff);
        
        // Calculate the amount of rotation to perform this frame
        float dRotation = Speed * Time.unscaledDeltaTime;
        float rotation = currentAngle + dir * dRotation;
        
        // If the rotation would take us past the target, stop right at the target
        if (dir < 0 && currentAngle - dRotation < targetAngle)
            rotation = targetAngle;
        else if (dir > 0 && currentAngle + dRotation > targetAngle)
            rotation = targetAngle;
        
        _rigidbody.SetRotation(rotation);
    }
}
