using UnityEngine;

public class DestroyJuice : JuiceEffect
{
    
    [SerializeField] private GameObject[] ObjectsToDestroy;
    [SerializeField] private float Delay;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Play()
    {
        foreach (GameObject go in ObjectsToDestroy)
            Destroy(go, Delay);
    }
}
