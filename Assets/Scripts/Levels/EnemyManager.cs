using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public static EnemyManager Instance;

    // List of currently active enemies
    private List<EnemyHealth> _enemies;
    
    void Awake()
    {
        if (Instance is null)
        {
            _enemies = new List<EnemyHealth>();
            Instance = this;
        }
        else
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
    
    public void Add(EnemyHealth enemy)
    {
        _enemies.Add(enemy);
    }

    public void Remove(EnemyHealth enemy)
    {
        _enemies.Remove(enemy);
    }

    public int Count()
    {
        return _enemies.Count;
    }
}
