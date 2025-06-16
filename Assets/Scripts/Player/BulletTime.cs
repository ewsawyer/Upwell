using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BulletTime : MonoBehaviour
{

    [Tooltip("The timescale to use when in bullet time")]
    [SerializeField] private float Scale;

    [Tooltip("The ring that indicates the cooldown")] [SerializeField]
    private Image CooldownRing;

    [Tooltip("The color of the ring when bullet time is unusable")] [SerializeField]
    private Color ColorUnusable;

    [Tooltip("The color of the ring when bullet time is usable")] [SerializeField]
    private Color ColorUsable;

    [Tooltip("Maximum amount of time you can stay in bullet time")] [SerializeField]
    private float MaxBulletTimeDuration;

    [Tooltip("Rate at which bullet time comes back in seconds per second")] [SerializeField]
    private float BulletTimeRegenerationRate;

    [Tooltip("Amount of time to pause before bullet time starts regenerating")] [SerializeField]
    private float RegenerationDelay;

    [Tooltip("Whether to allow the player to activate bullet time when it's not fully charged")] [SerializeField]
    private bool AllowUseWhenPartiallyCharged;

    private float _remainingBulletTime;
    private float _regenerationDelayTimer;
    private bool _isInBulletTime;

    private void Start()
    {
        _remainingBulletTime = MaxBulletTimeDuration;
    }
    
    private void Update()
    {
        UpdateRing();
        
        if (_isInBulletTime)
            InBulletTimeUpdate();
        else
            OutOfBulletTimeUpdate();
    }

    public bool IsUsable()
    {
        return AllowUseWhenPartiallyCharged || _remainingBulletTime >= MaxBulletTimeDuration;
    }

    private void UpdateRing()
    {
        CooldownRing.fillAmount = _remainingBulletTime / MaxBulletTimeDuration;
        CooldownRing.color = IsUsable() ? ColorUsable : ColorUnusable;
    }

    private void InBulletTimeUpdate()
    {
        _remainingBulletTime -= Time.unscaledDeltaTime;

        if (_remainingBulletTime <= 0.0f)
            EndBulletTime();
    }

    private void OutOfBulletTimeUpdate()
    {
        // Wait before regenerating bullet time
        if (_regenerationDelayTimer > 0.0f)
        {
            _regenerationDelayTimer -= Time.unscaledDeltaTime;
            return;
        }
        
        // Regenerate bullet time
        _remainingBulletTime += BulletTimeRegenerationRate * Time.unscaledDeltaTime;
        _remainingBulletTime = Mathf.Clamp(_remainingBulletTime, 0.0f, MaxBulletTimeDuration);
    }
    
    private void OnBulletTime(InputValue val)
    {
        if (val.isPressed && IsUsable())
            StartBulletTime();
        else if (_isInBulletTime) 
            EndBulletTime();
    }

    private void StartBulletTime()
    {
        TimeManager.Instance.ChangeTimeScale(Scale, interrupt: true);
        TimeManager.Instance.SetLocked(true);
        
        _isInBulletTime = true;
    }

    private void EndBulletTime()
    {
        _regenerationDelayTimer = RegenerationDelay;
        TimeManager.Instance.SetLocked(false);
        TimeManager.Instance.ChangeTimeScale(1.0f, interrupt: true);
        
        _isInBulletTime = false;
    }
}
