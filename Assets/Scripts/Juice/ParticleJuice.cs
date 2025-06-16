using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ParticleJuice : JuiceEffect
{
    [SerializeField] private ParticleSystem Particles;
    [SerializeField] private bool MatchParentRotation2D;
    [SerializeField] private float Delay;
    
    public override void Play()
    {
        StartCoroutine(PlayCoroutine());
    }

    private IEnumerator PlayCoroutine()
    {
        if (Delay > 0.0f)
            yield return new WaitForSecondsRealtime(Delay);
        
        ParticleSystem particles = Instantiate(Particles);
        particles.transform.position = transform.position;
        
        if (MatchParentRotation2D)
        {
            Vector3 parentRight = transform.parent.right;
            parentRight.z = 0;
            particles.transform.right = parentRight;
        }
    }
}
