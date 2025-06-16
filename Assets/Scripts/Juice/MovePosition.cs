using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MovePosition : JuiceEffect
{
    public enum Style
    {
        Offset,
        Absolute,
        ToTargetTransform
    }

    [SerializeField] private Transform ObjectToMove;
    [SerializeField] private Transform Target;
    [SerializeField] private float Delay;
    [SerializeField] private float Duration;
    [SerializeField] private Vector3 MoveTo;
    [SerializeField] private MovePosition.Style MoveStyle;
    [SerializeField] private AnimationCurve Curve;

    private float _timer;
    
    public override void Play()
    {
        StartCoroutine(MoveCoroutine());
    }

    public void PlayReversed()
    {
        StartCoroutine(MoveCoroutine(true));
    }

    public float GetDelay()
    {
        return Delay;
    }

    public float GetDuration()
    {
        return Duration;
    }

    public void SetObjectToMove(Transform t)
    {
        ObjectToMove = t;
    }
    public void SetTarget(Transform t)
    {
        Target = t;
    }


    public void SetMoveTo(Vector3 v)
    {
        MoveTo = v;
    }

    public IEnumerator MoveCoroutine(bool reversed=false)
    {
        if (Delay > 0.0f)
            yield return new WaitForSeconds(Delay);

        _timer = 0.0f;
        
        Vector3 startPos = ObjectToMove.transform.position;
        Vector3 endPos = reversed ? -MoveTo : MoveTo;
        if (MoveStyle == Style.Offset)
            endPos = startPos + endPos;
        
        while (_timer < Duration)
        {
            Vector3 pos;
            if (MoveStyle == Style.ToTargetTransform)
            {
                startPos = ObjectToMove.position;
                endPos = Target.position;
                Vector3 dir = (endPos - startPos).normalized;
                float dist = Vector3.Distance(startPos, endPos);
                float remainingTime = Duration - _timer;
                pos = startPos + (dist / remainingTime) * Time.deltaTime * dir;
            }
            else
            {
                pos = Vector3.Lerp(startPos, endPos, Curve.Evaluate(_timer / Duration));
            }
            
            ObjectToMove.transform.position = pos;
            
            _timer += Time.unscaledDeltaTime;
            yield return null;
        }

        ObjectToMove.transform.position = endPos;
    }
}
