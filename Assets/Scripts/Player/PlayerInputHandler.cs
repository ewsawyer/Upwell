using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    
    [Tooltip("Minimum magnitude of input for rotation")] [SerializeField]
    private float DeadzoneMagnitude;

    [Tooltip("Direction of mouse input will be relative to this transform")] [SerializeField]
    private Transform MouseInputAnchor;
    
    public Vector2 targetDir { get; private set; }
    public Quaternion targetRotation { get; private set; }
    
    private void OnRotateController(InputValue val)
    {
        Vector2 raw = val.Get<Vector2>();
        // If the player is inputting something set that as the target
        if (raw.magnitude > DeadzoneMagnitude)
            SetProperties(raw.normalized);
    }

    private void OnRotateMouse(InputValue val)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(val.Get<Vector2>());
        Vector2 dir = (worldPos - (Vector2)MouseInputAnchor.position).normalized;
        SetProperties(dir);
    }

    private void SetProperties(Vector2 inputDir)
    {
        targetDir = inputDir.normalized;
        targetRotation = Quaternion.LookRotation(Vector3.forward, targetDir);
    }
}
