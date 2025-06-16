using UnityEngine;

public class PlayerBoost : MonoBehaviour
{

    [SerializeField] private float DefaultBoost;

    private Rigidbody2D _rigidbody;
    private PlayerRotation _rotation;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rotation = GetComponent<PlayerRotation>();
    }
    
    public void Boost()
    {
        Boost(DefaultBoost);
    }
    
    public void Boost(float boost)
    {
        Vector2 dir = -_rotation.currentDirection;
        Vector2 force = TimeManager.Instance.Scale * boost * dir;
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
    }
}
