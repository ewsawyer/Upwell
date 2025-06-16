using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileLauncher : MonoBehaviour
{

    [SerializeField] private Projectile ProjectilePrefab;
    [SerializeField] private float RepeatFireDelay;
    [SerializeField] private Transform WeaponOrigin;
    [SerializeField] private float Damage;

    private float _delayTimer;
    private bool _holdingFire;
    private PlayerBoost _boost;
    private PlayerRotation _rotation;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _boost = GetComponent<PlayerBoost>();
        _rotation = GetComponent<PlayerRotation>();
    }

    // Update is called once per frame
    void Update()
    {
        // Count down to next shot
        if (_delayTimer > 0.0f)
            _delayTimer -= Time.unscaledDeltaTime;

        // If we can fire
        if (_holdingFire && _delayTimer <= 0.0f)
        {
            Fire();
            _delayTimer = RepeatFireDelay;
        }
    }

    private void OnFire(InputValue val)
    {
        _holdingFire = val.isPressed;
    }

    public void Fire()
    {
        _boost.Boost();
        Projectile p = Instantiate(ProjectilePrefab);
        p.Initialize(WeaponOrigin.position, _rotation.currentDirection);
    }
}
