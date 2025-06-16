using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Juice JuiceDestroy;
    [SerializeField] private float StartingHealth;
    [SerializeField] private int Points;
    
    public float health { get; private set; }

    public float GetStartingHealth()
    {
        return StartingHealth;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = StartingHealth;
        EnemyManager.Instance.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        // If the enemy is being destroyed because of scene cleanup, we don't need to do anything else
        if (!gameObject.scene.isLoaded)
            return;
        
        if (ScoreManager.Instance)
            ScoreManager.Instance.Score(Points);
        if (EnemyManager.Instance)
            EnemyManager.Instance.Remove(this);
    }

    public void Hit(float damage)
    {
        health -= damage;
        
        if (health <= 0.0f && JuiceDestroy)
            JuiceDestroy.Play();
    }
}
