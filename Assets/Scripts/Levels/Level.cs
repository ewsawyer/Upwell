using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Level : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI LevelNumberText;
    [SerializeField] private Transform SpawnerParent;

    [Tooltip("Walls are expected to be listed: Top, Right, Bottom, Left")]
    [SerializeField] private GameObject[] Walls;
    
    public int number { get; private set; }

    private EnemySpawner[] _spawners;
    
    public void SetNumber(int n)
    {
        number = n;
        LevelNumberText.text = "Level " + number;
    }

    public void Initialize(int levelNumber, int wallIndex)
    {
        // Set level number
        SetNumber(levelNumber);

        // Get spawners
        _spawners = SpawnerParent.GetComponentsInChildren<EnemySpawner>();
        // Spawn enemies
        foreach (EnemySpawner spawner in _spawners)
            spawner.Spawn();
        
        // Remove the appropriate wall
        RemoveWall(wallIndex);
    }

    public void RemoveWall(int index)
    {
        Walls[index].SetActive(false);
    }
}
