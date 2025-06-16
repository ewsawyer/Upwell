using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CinemachineShakeManger : MonoBehaviour
{
    public static CinemachineShakeManger Instance;

    private Coroutine _coroutineShake;
    private float _timer;
    private CinemachineBasicMultiChannelPerlin _noise;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _noise = (CinemachineBasicMultiChannelPerlin)GetComponent<CinemachineCamera>().GetCinemachineComponent(CinemachineCore.Stage.Noise);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void Shake(float amplitude, float frequency, float duration)
    {
        _noise.AmplitudeGain = amplitude;
        _noise.FrequencyGain = frequency;
        _timer = duration;

        if (_coroutineShake == null)
            _coroutineShake = StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        while (_timer > 0.0f)
        {
            _timer -= Time.deltaTime;
            yield return null;
        }

        _noise.AmplitudeGain = 0.0f;
        _noise.FrequencyGain = 0.0f;
        _coroutineShake = null;
    }
}
