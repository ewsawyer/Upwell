using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Juice JuiceDie;
    
    public bool isDead { get; private set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<EnemyHealth>() || collision.collider.GetComponent<Spike>())
            Hit();
    }

    public void Hit()
    {
        // player only has 1 hp for now. probably won't change
        Die();
    }

    public void Die()
    {
        if (isDead)
            return;
        
        isDead = true;
        JuiceDie.Play();
    }
}
