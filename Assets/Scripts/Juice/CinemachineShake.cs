using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CinemachineShake : JuiceEffect
{
    [SerializeField] private float Amplitude;
    [SerializeField] private float Frequency;
    [SerializeField] private float Duration;

    public override void Play()
    {
        CinemachineShakeManger.Instance.Shake(Amplitude, Frequency, Duration);
    }

}
