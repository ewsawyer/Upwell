using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventJuice : JuiceEffect
{
    [Tooltip("Delays the invocation of the event by this much")] [SerializeField]
    private float Delay;
    
    [Tooltip("The unity event to invoke when this juice plays")] [SerializeField]
    private UnityEvent Event;
    
    public override void Play()
    {
        StartCoroutine(PlayCoroutine());
    }

    private IEnumerator PlayCoroutine()
    {
        if (Delay > 0.0f)
            yield return new WaitForSecondsRealtime(Delay);
        
        Event.Invoke();
    }
}
