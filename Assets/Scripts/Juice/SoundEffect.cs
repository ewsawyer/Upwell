using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SoundEffect : JuiceEffect
{
    public enum ClipSelectionMode
    {
        Random,
        Sequence,
        First
    }
    
    [FormerlySerializedAs("Clip")] [Tooltip("The clips to play")] [SerializeField]
    private AudioClip[] Clips;

    [Tooltip("How to select which clip to play from the list")] [SerializeField]
    private ClipSelectionMode SelectionMode;

    [Tooltip("The minimum pitch to play the clip at")] [SerializeField]
    private float MinPitch;

    [Tooltip("The maximum pitch to play the clip at")] [SerializeField]
    private float MaxPitch;

    [Tooltip("The volume of the clip")] [SerializeField]
    private float EffectVolume = 1.0f;

    [Tooltip("Delay playing of the clip by this many seconds")] [SerializeField]
    private float Delay;

    [SerializeField] private bool UseRealtimeDelay;

    [Tooltip("Whether to loop the clip")] [SerializeField]
    private bool Loop;

    [Tooltip("Whether the clip should stop when this object is destroyed")] [SerializeField]
    private bool DestroyWithThisObject;
    
    [Tooltip("Whether to increase or decrease the pitch slightly with each play")] [SerializeField]
    private bool ScalePitch;

    [Tooltip("The amount of pitch to add to the sound effect each time its played")] [SerializeField]
    private float PitchIncrease;

    [Tooltip(
        "After not playing the sound for this many seconds or longer, the pitch will reset to the value of MinPitch")]
    [SerializeField]
    private float PitchResetCooldown;

    [Tooltip("The maximum amount of time the clip should last")] [SerializeField]
    private float MaxDuration = float.MaxValue;

    private int _soundIndex;
    private float _currentPitch;
    private float _pitchResetTimer;
    private AudioSource _source;

    private void Start()
    {
        _currentPitch = MinPitch;
    }

    private void Update()
    {
        if (_pitchResetTimer > 0.0f)
            _pitchResetTimer -= Time.deltaTime;
    }

    private void OnDestroy()
    {
        if (DestroyWithThisObject)
            Destroy(_source.gameObject);
    }

    public override void Play()
    {
        StartCoroutine(PlayCoroutine());
    }

    private IEnumerator PlayCoroutine()
    {
        float pitch;
        if (!ScalePitch)
            pitch = Random.Range(MinPitch, MaxPitch);
        // If we're scaling the pitch and need to add more
        else if (_pitchResetTimer > 0.0f)
        {
            pitch = _currentPitch;
            _currentPitch = Mathf.Clamp(_currentPitch + PitchIncrease, MinPitch, MaxPitch);
        }
        // If we need to reset the pitch because the timer has run out
        else
        {
            pitch = MinPitch;
            _currentPitch = MinPitch;
        }

        _pitchResetTimer = PitchResetCooldown;

        if (SelectionMode == ClipSelectionMode.First)
        {
            AudioManager.Instance.PlayClip(Clips[0], pitch, EffectVolume);
            yield break;
        }
        
        if (SelectionMode == ClipSelectionMode.Random)
            _soundIndex = Random.Range(0, Clips.Length);

        if (Delay > 0.0f)
        {
            if (UseRealtimeDelay)
                yield return new WaitForSecondsRealtime(Delay);
            else    
                yield return new WaitForSeconds(Delay);
        }
        
        _source = AudioManager.Instance.PlayClip(Clips[_soundIndex], pitch, EffectVolume, Loop, MaxDuration, false);

        if (SelectionMode == ClipSelectionMode.Sequence)
            _soundIndex = (_soundIndex + 1) % Clips.Length;

    }
}
