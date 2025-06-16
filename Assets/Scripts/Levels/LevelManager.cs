using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    [SerializeField] private Vector2 Offset;
    [SerializeField] private Level[] PrefabLevels;
    [SerializeField] private Level FirstLevel;
    
    public int level { get; private set; }

    private Level _currentLevel;
    private Vector2 _levelPos;
    private MovePosition _cameraConfines;
    private Coroutine _coroutineGrace;
    private List<Level> _levelBag;
    
    void Awake()
    {
        if (Instance is null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _cameraConfines = GameObject.FindWithTag("Camera Bounds").GetComponent<MovePosition>();
        _coroutineGrace = StartCoroutine(GracePeriod());
        StartFirstLevel();
        RefillBag();
    }
    public void RefillBag()
    {
        _levelBag = PrefabLevels.ToList();
    }

    public void StartFirstLevel()
    {
        level = 1;
        _currentLevel = InstantiateNewLevelPrefab(FirstLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (_coroutineGrace != null)
            return;
        
        if (EnemyManager.Instance.Count() == 0)
            NextLevel();
    }

    private void NextLevel()
    {
        level++;
        // Move the position of the level
        _levelPos += Offset.y * Vector2.up;
        // Move the camera
        _cameraConfines.SetMoveTo(_levelPos);
        _cameraConfines.Play();
        // Remove the top wall
        _currentLevel.RemoveWall(0);
        // Create and save the new level
        _currentLevel = InstantiateNewLevelPrefab();
    }

    private Level InstantiateNewLevelPrefab()
    {
        // If bag is empty, refill it
        if (_levelBag.Count == 0)
            RefillBag();
        
        // Select a random level prefab
        Level randomLevel = _levelBag[Random.Range(0, _levelBag.Count)];
        // Remove it from the bag
        _levelBag.Remove(randomLevel);
        // Instantiate it
        return InstantiateNewLevelPrefab(randomLevel);
    }

    private Level InstantiateNewLevelPrefab(Level toInstantiate)
    {
        // Instantiate the new level
        Level lvl = Instantiate(toInstantiate, _levelPos, Quaternion.identity);
        // Removing bottom wall by default
        lvl.Initialize(level, 2);

        return lvl;
    }

    private IEnumerator GracePeriod()
    {
        yield return new WaitForSeconds(1.0f);
        _coroutineGrace = null;
    }
}
