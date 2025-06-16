using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFlash : JuiceEffect
{
    [Tooltip("The sprite renderer to change the color of")] [SerializeField]
    private SpriteRenderer Target;

    [Tooltip("Other sprite renderers to change. I'll fix this in the next project")] [SerializeField]
    private SpriteRenderer[] OtherTargets;
    
    [Tooltip("Color to flash to when hit")] [SerializeField]
    private Color FlashColor;

    [Tooltip("Number of times to flash the desired color")] [SerializeField]
    private int NumFlashes;
    
    [Tooltip("Total duration of the effect")]
    [SerializeField] private float Duration;
    
    private Color _originalColor;
    private Color[] _otherOriginalColors;
    
    // Start is called before the first frame update
    void Start()
    {
        _otherOriginalColors = new Color[OtherTargets.Length];
    }
    
    public override void Play()
    {
        if (Target)
            _originalColor = Target.color;
        
        for (int i = 0; i < OtherTargets.Length; i++)
            _otherOriginalColors[i] = OtherTargets[i].color;

        if (OtherTargets.Length > 0)
            for (int i = 0; i < OtherTargets.Length; i++)
                StartCoroutine(FlashCoroutine(i));
        else
            StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        for (int i = 0; i < NumFlashes; i++)
        {
            Target.color = FlashColor;
            yield return new WaitForSecondsRealtime(Duration / NumFlashes / 2.0f);
            
            Target.color = _originalColor;
            yield return new WaitForSecondsRealtime(Duration / NumFlashes / 2.0f);
        }

        Target.color = _originalColor;
    }

    private IEnumerator FlashCoroutine(int index)
    {
        SpriteRenderer target = OtherTargets[index];
        Color originalColor = _otherOriginalColors[index];
        
        for (int i = 0; i < NumFlashes; i++)
        {
            target.color = FlashColor;
            yield return new WaitForSecondsRealtime(Duration / NumFlashes / 2.0f);
            
            target.color = originalColor;
            yield return new WaitForSecondsRealtime(Duration / NumFlashes / 2.0f);
        }

        target.color = originalColor;
    }
}
