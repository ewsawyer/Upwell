using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    
    [Tooltip("The timescale to return to after a timescale change")] [SerializeField]
    private float OriginalScale;

    public float Scale;
    public float Duration;

    private float _timer;
    private Coroutine _coroutineSlowdown;
    private bool _isLocked;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            ResetTimeScale();
        }
        else
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void SetLocked(bool locked)
    {
        _isLocked = locked;
    }

    public void ChangeTimeScale(float scale, float duration)
    {
        if (_isLocked)
            return;
        
        if (_coroutineSlowdown != null && duration > Duration - _timer)
        {
            Duration = _timer + duration;
            this.Scale = scale;
        }
        else if (_coroutineSlowdown == null)
        {
            Duration = duration;
            Scale = scale;
            _coroutineSlowdown = StartCoroutine(Slowdown());
        }
    }

    public void ChangeTimeScale(float scale, bool interrupt=true)
    {
        if (_isLocked)
            return;
        
        if (interrupt && _coroutineSlowdown != null)
            StopCoroutine(_coroutineSlowdown);
        
        this.Scale = scale;
        Time.timeScale = Scale;
        Time.fixedDeltaTime = 0.02f * Scale;
    }

    public void ResetTimeScale()
    {
        Scale = 1.0f;
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
    }
    
    private IEnumerator Slowdown()
    {
        _timer = 0.0f;

        while (_timer < Duration)
        {
            Time.timeScale = Scale;
            Time.fixedDeltaTime = 0.02f * Scale;
            _timer += Time.unscaledDeltaTime;
            yield return null;
        }

        ResetTimeScale();
        _coroutineSlowdown = null;
    }

}
