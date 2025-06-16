using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class FadeLineRenderer : JuiceEffect
{
    [SerializeField] private LineRenderer Target;
    
    [SerializeField] private bool PlayOnStart;
    [SerializeField] private float Delay;
    [SerializeField] private bool DestroyAfterFade;
    [SerializeField] private float Duration;
    [SerializeField] private float TargetOpacity;
    
    private float _timer;
    private float _currentDelay;
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayOnStart)
            StartCoroutine(FadeCoroutine());
    }

    public override void Play()
    {
        _currentDelay = Delay;
        StartCoroutine(FadeCoroutine());
    }

    public void SkipDelay(bool skip)
    {
        _currentDelay = skip ? 0.0f : Delay;
    }
    
    private IEnumerator FadeCoroutine()
    {
        Gradient g = Target.colorGradient;
        
        if (_currentDelay > 0.0f)
            yield return new WaitForSecondsRealtime(_currentDelay);
        
        float startOpacity = g.alphaKeys[0].alpha;
        _timer = 0.0f;

        while (_timer < Duration)
        {
            float opacity = Mathf.Lerp(startOpacity, TargetOpacity, _timer / Duration);
            SetOpacity(opacity);
            _timer += Time.unscaledDeltaTime;
            yield return null;
        }

        yield return null;
        
        if (DestroyAfterFade)
            Destroy(Target.gameObject);
    }

    private void SetOpacity(float opacity)
    {
        GradientColorKey[] colors = Target.colorGradient.colorKeys;
        GradientAlphaKey[] alphas = Target.colorGradient.alphaKeys;
        
        for (int i = 0; i < alphas.Length; i++)
            alphas[i].alpha = opacity;
        
        Target.colorGradient.SetKeys(colors, alphas);
    }
}
